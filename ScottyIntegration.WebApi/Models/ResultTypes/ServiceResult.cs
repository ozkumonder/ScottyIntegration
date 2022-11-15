using Newtonsoft.Json.Linq;

namespace ScottyIntegration.WebApi.Models.ResultTypes
{
    /// <summary>
    /// Servis Dönüş Sınıfı
    /// </summary>
    public class ServiceResult
    {
        public int LogRef
        {
            get;
            set;
        }
        public object ObjectNo
        {
            get;
            set;
        }

        public bool Success
        {
            get;
            set;
        }
        public int RowCount
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public int ErrorCode
        {
            get;
            set;
        }

        public string ErrorDesc
        {
            get;
            set;
        }
        /// <summary>
        /// JSON Result
        /// </summary>
        public JToken JResult { get; set; }

    }

}
