using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using ScottyIntegration.WebApi.Core.Helper;
using ScottyIntegration.WebApi.Core.Utilities;
using ScottyIntegration.WebApi.Models.Dtos;
using ScottyIntegration.WebApi.Models.ERPModels;
using ScottyIntegration.WebApi.Models.Global;
using ScottyIntegration.WebApi.Models.ResultTypes;

namespace ScottyIntegration.WebApi.Core.DataAccess
{
    public class DataLogic
    {
        private static string _error;
        private static DataAccess _dataAccess = new DataAccess(ConfigHelper.ConnectionStringWithConfigXml(ConfigHelper.ReadPath), ref _error);
        private static DataAccessException exception;
        /// <summary>
        /// Parametre verilen muhasebe konunun var olup olmadığı kontrolünü yapar
        /// </summary>
        /// <param name="emuAccCode"></param>
        /// <returns>true or false</returns>
        public static bool CheckEmuAccCode(string emuAccCode)
        {
            var flag = false;
            try
            {
                var sql = "IF EXISTS(SELECT * FROM LG_XXX_EMUHACC WHERE CODE = @CODE) SELECT 1 ISOK ELSE SELECT 0 ISOK".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", emuAccCode) };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
                if (result.ToInt32() > 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }

            return flag;
        }
        /// <summary>
        /// Parametre verilen proje konunun var olup olmadığı kontrolünü yapar
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns>true or false</returns>
        public static bool CheckProjectCard(string projectCode)
        {
            var flag = false;
            try
            {
                var sql = "IF EXISTS(SELECT * FROM LG_XXX_PROJECT WHERE CODE = @CODE) SELECT 1 ISOK ELSE SELECT 0 ISOK".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", projectCode) };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
                if (result.ToInt32() > 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }

            return flag;
        }
        /// <summary>
        /// Parametre verilen hizmet konunun var olup olmadığı kontrolünü yapar
        /// </summary>
        /// <param name="srvCode"></param>
        /// <returns>true or false</returns>
        public static bool CheckSrvCard(string srvCode)
        {
            var flag = false;
            try
            {
                var sql = "IF EXISTS(SELECT * FROM LG_XXX_SRVCARD WHERE CODE = @CODE) SELECT 1 ISOK ELSE SELECT 0 ISOK".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", srvCode) };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
                if (result.ToInt32() > 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }

            return flag;
        }
        /// <summary>
        /// Parametre verilen cari hesap konunun var olup olmadığı kontrolünü yapar
        /// </summary>
        /// <param name="clCode"></param>
        /// <returns>true or false</returns>
        public static bool CheckClCard(string clCode)
        {
            var flag = false;
            try
            {
                var sql = "IF EXISTS(SELECT * FROM LG_XXX_CLCARD WHERE CODE = @CODE) SELECT 1 ISOK ELSE SELECT 0 ISOK".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", clCode) };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
                if (result.ToInt32() > 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }

            return flag;
        }
        public static ClientInfoDto CheckClientWithTaxOrTckn(string taxNumber)
        {
            var result = new ClientInfoDto();
            try
            {
                var sql = "IF EXISTS(SELECT * FROM LG_XXX_CLCARD WHERE (TAXNR = @TAXNR OR TCKNO = @TCKNO)) SELECT 1 ISOK,LOGICALREF,CODE FROM LG_XXX_CLCARD WHERE (TAXNR = @TAXNR OR TCKNO = @TCKNO) ELSE SELECT 0 ISOK,0 'LOGICALREF','' 'CODE'".ToReplaceLogoTableName();
                var parms = new[]
                {
                    new SqlParameter("@TAXNR", taxNumber??string.Empty),
                    new SqlParameter("@TCKNO", taxNumber??string.Empty)
                };
                result = _dataAccess.GetDataTableByParams(sql, parms, ref exception).AsEnumerable().Select(s => new ClientInfoDto
                {
                    Lref = s.Field<int>("LOGICALREF"),
                    Code = s.Field<string>("CODE"),
                    CheckingResult = s.Field<int>("ISOK").ToBool()
                }).FirstOrDefault();
                //if (result.ToInt32() > 0)
                //{
                //    flag = true;
                //}
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }

            return result;
        }
        public static bool CheckInvoice(string invoiceNo)
        {
            var flag = false;
            try
            {
                var sql = "IF EXISTS(SELECT * FROM LG_XXX_CLCARD WHERE CODE = @CODE) SELECT 1 ISOK ELSE SELECT 0 ISOK".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", invoiceNo) };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
                if (result.ToInt32() > 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }

            return flag;
        }

        /// <summary>
        /// Cari Hesabın efatura kontrolünü yapar
        /// </summary>
        /// <param name="clCode"></param>
        /// <returns></returns>
        public static bool CheckClientEInvoice(string clCode)
        {
            var flag = false;
            try
            {
                var sql = "IF EXISTS(SELECT * FROM LG_XXX_CLCARD WHERE ACCEPTEINV = 1 AND CODE = @CODE) SELECT 1 ISOK ELSE SELECT 0 ISOK".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", clCode) };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
                if (result.ToInt32() > 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }

            return flag;
        }
        /// <summary>
        /// Cari Hesabın earşiv kontrolünü yapar
        /// </summary>
        /// <param name="clCode"></param>
        /// <returns></returns>
        public static bool CheckClientEArchive(string clCode)
        {
            var flag = false;
            try
            {
                var sql = "IF EXISTS(SELECT * FROM LG_XXX_CLCARD WHERE ACCEPTEINV = 1 AND CODE = @CODE) SELECT 1 ISOK ELSE SELECT 0 ISOK".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", clCode) };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
                if (result.ToInt32() <= 0)
                {
                    flag = true;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }

            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clCode"></param>
        /// <returns></returns>
        public static object GetClientRefByCode(string clCode)
        {
            object result = string.Empty;
            try
            {
                var sql = "SELECT LOGICALREF FROM LG_XXX_CLCARD WHERE CODE = @CODE".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", clCode) };
                result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
            }
            catch (System.Exception e)
            {

                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }
            return result;
        }
        public static object GetClientCodeByRef(int lref)
        {
            object result = string.Empty;
            try
            {
                var sql = "SELECT CODE FROM LG_XXX_CLCARD WHERE LOGICALREF = @LOGICALREF".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@LOGICALREF", lref) };
                result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
            }
            catch (System.Exception e)
            {

                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }
            return result;
        }
        public static object GetServiceCardRefByCode(string clCode)
        {
            object result = string.Empty;
            try
            {
                var sql = "SELECT LOGICALREF FROM LG_XXX_SRVCARD WHERE CODE = @CODE".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", clCode) };
                result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
            }
            catch (System.Exception e)
            {

                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }
            return result;
        }
        public static object GetProjectCardRefByCode(string clCode)
        {
            object result = string.Empty;
            try
            {
                var sql = "SELECT LOGICALREF FROM LG_XXX_PROJECT WHERE CODE = @CODE".ToReplaceLogoTableName();
                var parms = new[] { new SqlParameter("@CODE", clCode) };
                result = _dataAccess.ExecuteScalar(sql, parms, ref exception);
            }
            catch (System.Exception e)
            {

                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }
            return result;
        }
        public static string GetBnFicheNumberByRef(int lref, ref DataAccessException exception)
        {
            var result = string.Empty;
            string sql = @"SELECT FICHENO FROM LG_XXX_XX_BNFICHE WHERE LOGICALREF = @LREF".ToReplaceLogoTableName();
            var parms = new[]
            {
                new SqlParameter("@LREF", lref)
            };
            result = _dataAccess.GetDataTableByParams(sql, parms, ref exception).AsEnumerable().Select(s => s.Field<string>("FICHENO")).FirstOrDefault();

            return result;
        }
        public static string GetClFicheNumberByRef(int lref)
        {
            var result = string.Empty;
            string sql = @"SELECT FICHENO FROM LG_XXX_XX_CLFICHE WHERE LOGICALREF = @LREF".ToReplaceLogoTableName();
            var parms = new[]
            {
                new SqlParameter("@LREF", lref)
            };
            result = _dataAccess.GetDataTableByParams(sql, parms, ref exception).AsEnumerable().Select(s => s.Field<string>("FICHENO")).FirstOrDefault();

            return result;
        }

        public static string GetLastClientCode()
        {
            var result = string.Empty;
            try
            {
                string sql = @"SELECT 'M1201.' + RIGHT('000000'+ISNULL(CONVERT(VARCHAR(6),MAX(CONVERT(INT,SUBSTRING(CODE,LEN(CODE)-3,6))) + 1),'1'),6) AS CODE FROM LG_221_CLCARD WITH(NOLOCK) WHERE CODE LIKE 'M1201.%'".ToReplaceLogoTableName();
                result = _dataAccess.GetDataTable(sql, ref exception).AsEnumerable().Select(s => s.Field<string>("CODE")).FirstOrDefault();
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error.ToLogType(), " ",
                    nameof(DataLogic), " ", MethodBase.GetCurrentMethod()?.Name, " ", (object)e.Message ?? "Hata"));
            }

            return result ?? "~";
        }

        public static ServiceResult InsertPostLog(PostLog postLog)
        {
            var flag = new ServiceResult();
            try
            {
                var query = "INSERT INTO BTI_POSTLOG(HOSTIP,POSTDATE,IDENTITYNAME,OPERATIONTYPE,URL,REQUESTMETHOD,JSONDATA) VALUES (@HOSTIP,@POSTDATE,@IDENTITYNAME,@OPERATIONTYPE,@URL,@REQUESTMETHOD,@JSONDATA)";
                var sql = query + ";SELECT SCOPE_IDENTITY();";
                var parms = new[]
                {
                new SqlParameter("@ID",postLog.Id),
                new SqlParameter("@HOSTIP", postLog.HostIp??string.Empty),
                new SqlParameter("@POSTDATE",postLog.PostDate),
                new SqlParameter("@IDENTITYNAME",postLog.IdentityName??string.Empty),
                new SqlParameter("@OPERATIONTYPE",postLog.OperationType??string.Empty),
                new SqlParameter("@URL",postLog.Url??string.Empty),
                new SqlParameter("@REQUESTMETHOD",postLog.RequestMethod??string.Empty),
                new SqlParameter("@JSONDATA",postLog.JsonData??string.Empty)
            };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception).ToInt32();
                if (result > 0)
                {
                    flag.Success = true;
                    flag.LogRef = result;
                }
                else
                {
                    flag.ErrorDesc = exception.ErrorDesc;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }


            return flag;
        }
        public static string GetInvoiceNumberByRef(int lref)
        {
            var result = string.Empty;
            try
            {
                string sql = @"SELECT FICHENO FROM LG_XXX_XX_INVOICE WHERE LOGICALREF = @LREF".ToReplaceLogoTableName();
                var parms = new[]
                {
                new SqlParameter("@LREF", lref)
            };
                result = _dataAccess.GetDataTableByParams(sql, parms, ref exception).AsEnumerable().Select(s => s.Field<string>("FICHENO")).FirstOrDefault();

            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }

            return result;
        }
        public static ServiceResult InsertResponseLog(ResponseLog responseLog)
        {
            var flag = new ServiceResult();
            try
            {
                var query = "INSERT INTO BTI_RESPONSELOG(POSTREF,HOSTIP,POSTDATE,IDENTITYNAME,OPERATIONTYPE,RESPONSESTATUS,JSONDATA,RESPONSEDATA) VALUES (@POSTREF,@HOSTIP,@POSTDATE,@IDENTITYNAME,@OPERATIONTYPE,@RESPONSESTATUS,@JSONDATA,@RESPONSEDATA)";
                var sql = query + ";SELECT SCOPE_IDENTITY();";
                var parms = new[]
                {
                new SqlParameter("@POSTREF",responseLog.PostId),
                new SqlParameter("@HOSTIP", responseLog.HostIp??string.Empty),
                new SqlParameter("@POSTDATE",responseLog.PostDate),
                new SqlParameter("@IDENTITYNAME",responseLog.IdentityName??string.Empty),
                new SqlParameter("@OPERATIONTYPE",responseLog.OperationType??string.Empty),
                new SqlParameter("@RESPONSESTATUS",responseLog.ResponseStatus),
                new SqlParameter("@JSONDATA",responseLog.JsonData??string.Empty),
                new SqlParameter("@RESPONSEDATA",responseLog.ResponseData??string.Empty)
            };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception).ToInt32();
                if (result > 0)
                {
                    flag.Success = true;
                    flag.LogRef = result;
                }
                else
                {
                    flag.ErrorDesc = exception.ErrorDesc;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
            }


            return flag;
        }
        public static ServiceResult InsertErrorLog(ErrorLog errorLog, ref DataAccessException dataAccessException)
        {
            var flag = new ServiceResult();
            try
            {
                var query = "INSERT INTO BTI_ERRORLOG(POSTREF,HOSTIP,POSTDATE,IDENTITYNAME,OPERATIONTYPE,ERRORCLASSNAME,ERRORMETHODNAME,ERRORMESSAGE,INNEREXCEPTION,JSONDATA,RESPONSEDATA) VALUES (@POSTREF,@HOSTIP,@POSTDATE,@IDENTITYNAME,@OPERATIONTYPE,@ERRORCLASSNAME,@ERRORMETHODNAME,@ERRORMESSAGE,@INNEREXCEPTION,@JSONDATA,@RESPONSEDATA)";
                var sql = query + ";SELECT SCOPE_IDENTITY();";
                var parms = new[]
                {
                new SqlParameter("@POSTREF",errorLog.PostId),
                new SqlParameter("@HOSTIP", errorLog.HostIp??string.Empty),
                new SqlParameter("@POSTDATE",errorLog.PostDate),
                new SqlParameter("@IDENTITYNAME",errorLog.IdentityName??string.Empty),
                new SqlParameter("@OPERATIONTYPE",errorLog.OperationType??string.Empty),
                new SqlParameter("@ERRORCLASSNAME",errorLog.ErrorClassName??string.Empty),
                new SqlParameter("@ERRORMETHODNAME",errorLog.ErrorMethodName??string.Empty),
                new SqlParameter("@ERRORMESSAGE",errorLog.ErrorMessage??string.Empty),
                new SqlParameter("@INNEREXCEPTION",errorLog.InnerException??string.Empty),
                new SqlParameter("@JSONDATA",errorLog.JsonData??string.Empty),
                new SqlParameter("@RESPONSEDATA",errorLog.ResponseData??string.Empty)
            };
                var result = _dataAccess.ExecuteScalar(sql, parms, ref exception).ToInt32();
                if (result > 0)
                {
                    flag.Success = true;
                    flag.LogRef = result;
                }
                else
                {
                    flag.ErrorDesc = exception.ErrorDesc;
                }
            }
            catch (Exception e)
            {
                LogHelper.LogError(string.Concat(LogHelper.LogType.Error, nameof(DataLogic), " ", MethodBase.GetCurrentMethod().Name, " ", e.Message));
                exception.ErrorDesc = e.Message;
            }


            return flag;
        }

        #region Exchange Prosess
        public static Exchange GetDailyExchange(int currType, DateTime date)
        {
            var firm = ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).LogoFirmNumber;
            var query = "SELECT SEPEXCHTABLE FROM L_CAPIFIRM WHERE NR = @NR";
            var parms = new[]
            {
                new SqlParameter("@NR", firm)
            };
            var result = new Exchange();
            var flag = _dataAccess.GetDataTableByParams(query, parms, ref exception).AsEnumerable().Select(s => s.Field<short>("SEPEXCHTABLE")).FirstOrDefault();

            string sql = string.Concat(@"SELECT LREF, dbo.fn_LogoDatetoSystemDate(DATE_)DATE_,RATES1,RATES2,RATES3,RATES4,EDATE FROM ", flag == 0 ? " L_DAILYEXCHANGES" : flag == 1 ? "LG_EXCHANGE_XXX" : "L_DAILYEXCHANGES", $" WITH(NOLOCK) WHERE CRTYPE IN ({currType}) AND (DATE_ = DAY('{date.ToString("yyyyMMdd")}') + 256 * MONTH('{date.ToString("yyyyMMdd")}') + 65536 * YEAR('{date.ToString("yyyyMMdd")}'))").ToReplaceLogoTableName();

            result = _dataAccess.GetDataTable(sql, ref exception).AsEnumerable().Select(s => new Exchange
            {
                LREF = s.Field<int>("LREF"),
                DATE_ = s.Field<DateTime>("DATE_"),
                RATES1 = s.Field<double>("RATES1"),
                RATES2 = s.Field<double>("RATES2"),
                RATES3 = s.Field<double>("RATES3"),
                RATES4 = s.Field<double>("RATES4"),
                EDATE = s.Field<DateTime>("EDATE")
            }).FirstOrDefault();
            if (result == null)
            {
                result = new Exchange
                {
                    RATES1 = 1,
                    RATES2 = 1,
                    RATES3 = 1,
                    RATES4 = 1,
                };
            }

            return result;
        }
        public static byte IsThereDailyExchange()
        {
            var result = 0;
            string sql = @"SELECT COUNT(*) EXCHANGE  FROM LG_EXCHANGE_XXX WITH(NOLOCK) WHERE CRTYPE IN (1) AND (DATE_ = DAY(GETDATE()) + 256 * MONTH(GETDATE()) + 65536 * YEAR(GETDATE()))".ToReplaceLogoTableName();

            result = _dataAccess.GetDataTable(sql, ref exception).AsEnumerable().Select(s => s.Field<int>("EXCHANGE")).FirstOrDefault();

            return result.ToByte();
        }


        #endregion

    }
}
