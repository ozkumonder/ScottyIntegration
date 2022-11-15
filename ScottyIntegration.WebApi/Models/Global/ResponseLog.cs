using System;

namespace ScottyIntegration.WebApi.Models.Global
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseLog
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string HostIp { get; set; }
        public DateTime PostDate { get; set; }
        public string IdentityName { get; set; }
        public string OperationType { get; set; }
        public bool ResponseStatus { get; set; }
        public string JsonData { get; set; }
        public string ResponseData { get; set; }
    }
}