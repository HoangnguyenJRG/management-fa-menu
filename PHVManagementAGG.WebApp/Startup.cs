using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using PHVManagementAGG.Core.Services;
using PHVManagementAGG.Core.Settings;
using PHVManagementAGG.WebApp.Middleware;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace PHVManagementAGG.WebApp
{
    public class Startup
    {        
        public IConfiguration configuration { get; }
        private readonly IWebHostEnvironment hostingEnvironment;        
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration,
                       IWebHostEnvironment hostingEnvironment
                       )
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;            
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson();
            services.AddHttpContextAccessor();

            #region GZIP Compression
            services.Configure<GzipCompressionProviderOptions>
                (options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = new string[]{
                                    "text/html",
                                    "text/css",
                                    "text/plain",
                                    "application/json",
                                    "application/javascript",
                                    "text/javascript"};
            });

            #endregion

            #region CORS
            /*MS**/
            var CORS = configuration.GetSection("appSettings");
            string[] originArray = CORS["CORS"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(originArray)
                                                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                                                        .AllowAnyHeader()
                                                        .AllowAnyMethod();
                                  });
            });
            /*End MS*/
            #endregion

            #region alow return NewtonsoftJson Object
            services.AddControllers()
                .AddNewtonsoftJson(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            #endregion

            #region Config Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PHVManagementAGG API",
                    Version = "v1"
                });               
            });
            #endregion
            
            services.AddDependencies(configuration, hostingEnvironment.ContentRootPath);
            services.AddMemoryCache(options =>
            {
                options.SizeLimit = 1024;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PHVManagementAGG.WebApp v1"));
            }

            app.UseHeaderVerificationMiddleware();

            //app.UseErrorHandler();

            app.Use(async (context, next) =>
            {
                await next.Invoke();
            });

            app.UseHttpsRedirection();

            app.UseResponseCompression();

            app.UseStaticFiles();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
