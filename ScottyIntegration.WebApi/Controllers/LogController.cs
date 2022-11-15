using System;
using ScottyIntegration.WebApi.Core.DataAccess;
using ScottyIntegration.WebApi.Models.Global;

namespace ScottyIntegration.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LogController
    {
        private static DataAccessException exception;
        internal int InsertPostLog(DateTime postDate, string hostAddress, string requestIdentityName, string requestFilePath, string url, string requestMethod, string jsonData)
        {
            return DataLogic.InsertPostLog(new PostLog
            {
                PostDate = DateTime.Now,
                HostIp = hostAddress,
                IdentityName = requestIdentityName,
                OperationType = requestFilePath,
                Url = url,
                RequestMethod = requestMethod,
                JsonData = jsonData
            }).LogRef;
        }
        internal int InsertPostLog(RequestDto requestDto)
        {
            return DataLogic.InsertPostLog(new PostLog
            {
                PostDate = requestDto.RequestDate,
                HostIp = requestDto.RequestIp,
                IdentityName = requestDto.UserIdentity,
                OperationType = requestDto.RequestUrl,
                Url = requestDto.RequestUrl,
                RequestMethod = requestDto.RequestMethod,
                JsonData = requestDto.RequestData
            }).LogRef;
        }


        internal int InsertResponseLog(int postId, string hostAddress, string requestIdentityName, string requestFilePath, string jsonData, string responseJsonData)
        {
            return DataLogic.InsertResponseLog(new ResponseLog
            {
                PostId = postId,
                HostIp = hostAddress,
                OperationType = requestFilePath,
                IdentityName = requestIdentityName,
                PostDate = DateTime.Now,
                ResponseStatus = false,
                JsonData = jsonData,
                ResponseData = responseJsonData
            }).LogRef;
        }

        internal int InsertErrorLog(int postId, string hostAddress, string requestIdentityName, string requestFilePath, string className, string methodName, string message, string jsonData, string responseJsonData)
        {
            return DataLogic.InsertErrorLog(new ErrorLog
            {
                PostId = postId,
                HostIp = hostAddress,
                OperationType = requestFilePath,
                IdentityName = requestIdentityName,
                PostDate = DateTime.Now,
                ErrorClassName = className,
                ErrorMethodName = methodName,
                ErrorMessage = message,
                JsonData = jsonData,
                ResponseData = responseJsonData
            }, ref exception).LogRef;
        }

    }
}
