using System;

namespace ScottyIntegration.WebApi.Models.Global
{
    public class RequestDto
    {
        public DateTime RequestDate { get; set; }
        public string RequestIp { get; set; }
        public string UserIdentity { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string RequestData { get; set; }

    }
}
