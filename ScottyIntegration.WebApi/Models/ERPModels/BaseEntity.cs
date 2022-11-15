using System;
using System.Text.Json.Serialization;

namespace ScottyIntegration.WebApi.Models.ERPModels
{
    /// <summary>
    /// Logo Rest Aktarım Parametreleri SInıfı
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Aktarım Parametreleri
        /// </summary>
        [Obsolete]
        [JsonIgnore]
        public DataObjectParameter DataObjectParameter { get; set; }
    }
}
