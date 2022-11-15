using ScottyIntegration.WebApi.Core.Helper;
using ScottyIntegration.WebApi.Core.Utilities;

namespace ScottyIntegration.WebApi.Core.DataAccess
{
    public class DataConfig
    {
        private static string _error;
        private static DataAccess _dataAccess = new DataAccess(ConfigHelper.ConnectionStringWithConfigXml(ConfigHelper.ReadPath), ref _error);
        private static DataAccessException exception;

        public static bool CheckAndCreateDBObjects(out string message)
        {
            bool flag = true;
            message = string.Empty;
            if (!_dataAccess.CheckIfTableExists("BTI_USERS"))
            {
                CreateTable("BTI_USERS", out message);
                LogHelper.LogError(message);
                
            }
            if (!_dataAccess.CheckIfTableExists("BTI_POSTLOG".ToReplaceLogoTableName()))
            {
                CreateTable("BTI_POSTLOG", out message);
                LogHelper.LogError(message);
            }
            if (!_dataAccess.CheckIfTableExists("BTI_RESPONSELOG".ToReplaceLogoTableName()))
            {
                CreateTable("BTI_RESPONSELOG", out message);
                LogHelper.LogError(message);
            }
            if (!_dataAccess.CheckIfTableExists("BTI_ERRORLOG".ToReplaceLogoTableName()))
            {
                CreateTable("BTI_ERRORLOG", out message);
                LogHelper.LogError(message);
            }
            return flag;
        }
        private static bool CreateTable(string tableName, out string message)
        {
            bool flag = true;
            message = string.Empty;
            string empty;
            if (tableName == "BTI_USERS")
            {
                empty = "CREATE TABLE BTI_USERS (LREF INT NOT NULL IDENTITY(1,1) PRIMARY KEY, COMPANY VARCHAR(50) NULL,USERNAME VARCHAR(50) NULL,PASSWORD VARCHAR(50) NULL,EMAIL VARCHAR(50) NULL)";
                flag = _dataAccess.ExecuteScalar(empty.ToReplaceLogoTableName(), ref exception).ToBool();
            }
            else if (tableName == "BTI_POSTLOG")
            {
                empty = "CREATE TABLE BTI_POSTLOG(LREF INT NOT NULL IDENTITY(1,1) PRIMARY KEY,HOSTIP VARCHAR(15) NULL,POSTDATE DATETIME NULL,OPERATIONTYPE VARCHAR(50) NULL,IDENTITYNAME VARCHAR(50) NULL URL VARCHAR(150) NULL,REQUESTMETHOD VARCHAR(10) NULL,JSONDATA NVARCHAR(MAX) NULL)";
                flag = _dataAccess.ExecuteScalar(empty.ToReplaceLogoTableName(), ref exception).ToBool();
            }
            else if (tableName == "BTI_RESPONSELOG")
            {
                empty = "CREATE TABLE BTI_RESPONSELOG(LREF INT NOT NULL IDENTITY(1,1) PRIMARY KEY,POSTREF INT NULL,HOSTIP VARCHAR(15) NULL,POSTDATE DATETIME NULL,OPERATIONTYPE VARCHAR(50) NULL,IDENTITYNAME VARCHAR(50) NULL,RESPONSESTATUS BIT NULL,JSONDATA NVARCHAR(MAX) NULL,RESPONSEDATA NVARCHAR(MAX) NULL)";
                flag = _dataAccess.ExecuteScalar(empty.ToReplaceLogoTableName(), ref exception).ToBool();
            }
            else if (tableName == "BTI_ERRORLOG")
            {
                empty = "CREATE TABLE BTI_ERRORLOG(LREF INT NOT NULL IDENTITY(1,1) PRIMARY KEY,HOSTIP VARCHAR(15) NULL,POSTDATE DATETIME NULL,OPERATIONTYPE VARCHAR(50) NULL,IDENTITYNAME VARCHAR(50) NULL,ERRORCLASSNAME VARCHAR(50) NULL,ERRORMETHODNAME VARCHAR(50) NULL,ERRORMESSAGE VARCHAR(MAX) NULL,INNEREXCEPTION VARCHAR(MAX) NULL,JSONDATA NVARCHAR(MAX) NULL,RESPONSEDATA NVARCHAR(MAX) NULL)";
                flag = _dataAccess.ExecuteScalar(empty.ToReplaceLogoTableName(), ref exception).ToBool();
            }
            if (!string.IsNullOrEmpty(message))
            {
                flag = false;
            }
            return flag;
        }
    }
}
