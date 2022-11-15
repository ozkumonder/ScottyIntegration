using System;

namespace ScottyIntegration.WebApi.Core.LogoRestIntegretion
{
    public class TokenHolder
    {
        private static TokenHolder instance;

        public int ExpireSeconds { get; set; }

        public bool IsValid
        {
            get { return DateTimeOffset.Now < this.ValidUntil; }
        }

        public bool IsLoggedIn { get; set; }
        public string Token { get; set; }
        public string Url { get; set; }
        public string FormData { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTimeOffset ValidUntil { get; set; }

        static TokenHolder()
        {
            TokenHolder.instance = new TokenHolder();
        }

        private TokenHolder()
        {
        }

        public static TokenHolder GetInstance()
        {
            return TokenHolder.instance;
        }
    }
}