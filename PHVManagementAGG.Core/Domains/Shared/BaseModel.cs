namespace PHVManagementAGG.Core.Domains.Shared
{
    public class BaseModel
    {
        public string ProjectId { get; set; }

        public string Lang { get; set; }

        public string Authorization { get; set; }

        public string Token { get; set; }

        public string User{ get; set; }
    }

    public class BaseErrorMessageResponse
    {
        public bool rStatus { get; set; }

        public string ErrorNumber { get; set; }

        public string ErrorSeverity { get; set; }

        public string ErrorState { get; set; }

        public string ErrorLine { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class BaseImageRequest
    {
        public string ImageBase64 { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
    }

    public class DaysOfWeek
    {
        public enum DayId
        {
            Sunday = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6
        }

        private DayId _currentDay;

        public DaysOfWeek(DayId day)
        {
            _currentDay = day;
        }

        public int GetDayId()
        {
            return (int)_currentDay;
        }

        public override string ToString()
        {
            return _currentDay.ToString();
        }

        public static bool IsValidDayId(int id)
        {
            return id >= 0 && id <= 6;
        }
    }
}
