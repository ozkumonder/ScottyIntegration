using System;

namespace ScottyIntegration.WebApi.Models.ERPModels
{
    public class Exchange
    {
        public int LREF { get; set; }
        public DateTime DATE_ { get; set; }
        public double RATES1 { get; set; }
        public double RATES2 { get; set; }
        public double RATES3 { get; set; }
        public double RATES4 { get; set; }
        public DateTime EDATE { get; set; }
    }
}