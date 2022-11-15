using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScottyIntegration.WebApi.Models.TokenModels
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        //[JsonPropertyName("refresh_token")]
        //public string RefreshToken { get; set; }
        [JsonPropertyName("expire_in")]
        public string ExpireIn { get; set; }
    }
}
