using System;

namespace ScottyIntegration.WebApi.Models.Global
{
    public class ConfigSettings
    {
        #region SQL Info
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }

        public string LogoServerName { get; set; }
        public string LogoDatabaseName { get; set; }
        public string LogoDbUserName { get; set; }
        public string LogoDbPassword { get; set; }

        #endregion

        #region Logo Info
        public string LogoUserName { get; set; }
        public string LogoPassword { get; set; }
        public string LogoFirmNumber { get; set; }
        public string LogoPeriodNumber { get; set; }
        public string LogoFilePath { get; set; }
        #endregion

        #region Rest Info
        public string RestClientId { get; set; }
        public string RestClientSecret { get; set; }
        public string RestServiceUrl { get; set; }
        public float RestTimeOut { get; set; }

        #endregion

        #region Connect Info
        public string ConnectUser { get; set; }
        public string ConnectPass { get; set; }
        public short ConnectWorkSpace { get; set; }
        #endregion

        #region E-Logo Info
        public string eLogoUserName { get; set; }
        public string eLogoPassword { get; set; }

        #endregion

        #region Ftp Info
        public string FtpHost { get; set; }
        public string FtpUser { get; set; }
        public string FtpPassword { get; set; }
        public string FilePath { get; set; }
        public string FilePrefix { get; set; }
        #endregion

        #region Temp Info
        public string TempPath { get; set; }

        #endregion

        #region Scheduler Info

        public int SchedulerType { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskRunTime { get; set; }
        public int TaskRepeatCount { get; set; }
        public bool TaskRunAlways { get; set; }
        //public DateTime TaskDailyParameter { get; set; }
        //public WeekDays TaskWeeklyParameter { get; set; }
        public int TaskMonthlyParameter { get; set; }
        #endregion



    }

}
