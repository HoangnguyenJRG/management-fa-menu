using System.ComponentModel;

namespace PHVManagementAGG.Core.Enums
{
    public enum ErrorCodes
    {
         

        InvalidHeader = 406,

        [Description("Success")]
        Success = 0,

        [Description("Bad Request")]
        BadRequest = 1,

        [Description("Bad Request")]
        ErrorRequest = 400,


        [Description("Invalid Model")]
        InvalidModel = 2,

        [Description("Entity is archived")]
        EntityIsArchived = 3,

        [Description("Internal Server Error")]
        InternalServerError = 4,

        [Description("Data not found")]
        EntityNotFound = 5,

        [Description("Account not found")]
        AccountNotFound = 6,

        [Description("Could not create user account")]
        CouldNotCreateAccount = 7,

        [Description("Email đã tồn tại.")]
        EmailAlreadyExists = 8,

        [Description("User đã tồn tại.")]
        AccountExist = 9,

        [Description("LDAP Authentication failed, check username and password.")]
        LoginLDAPFailed = 10,

        [Description("The Username or Password is Incorrect. Please contact IT for support.")]
        AccountIsIncorrect = 11,

        [Description("The account is blocked. Please contact IT for support.")]
        AccountIsBlocked = 12,

        [Description("The account is not active. Please contact IT for support.")]
        AccountNotActived = 13,

        // Bussiness
        [Description("Data not found")]
        DataNotFound = 14,
        
    }

}
