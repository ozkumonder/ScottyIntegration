using System;

namespace ScottyIntegration.WebApi.Core.DataAccess
{
    public class DataAccessException : Exception
    {
        public string ErrorDesc;

        public int ErrorNr
        {
            get;
            set;
        }

        public DataAccessException(int errorNr)
        {
            this.ErrorNr = errorNr;
            this.ErrorDesc = "";
        }
    }
}
