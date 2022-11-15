using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScottyIntegration.WebApi.Models.ERPModels
{
    public class ArpSlip : BaseEntity
    {
        public ArpSlip()
        {
            TRANSACTIONS = new ArpSlipsTransactions();
        }
        [JsonProperty("INTERNAL_REFERENCE")]
        public int INTERNAL_REFERENCE { get; set; }

        [JsonProperty("NUMBER")]
        public string NUMBER { get; set; }

        [JsonProperty("DATE")]
        public DateTime DATE { get; set; }

        [JsonProperty("TYPE")]
        public int TYPE { get; set; }

        [JsonProperty("AUXIL_CODE")]
        public string AUXIL_CODE { get; set; }

        [JsonProperty("AUTH_CODE")]
        public string AUTH_CODE { get; set; }

        [JsonProperty("DIVISION")]
        public int DIVISION { get; set; }

        [JsonProperty("DEPARTMENT")]
        public int DEPARTMENT { get; set; }

        [JsonProperty("NOTES1")]
        public string NOTES1 { get; set; }

        [JsonProperty("NOTES2")]
        public string NOTES2 { get; set; }

        [JsonProperty("NOTES3")]
        public string NOTES3 { get; set; }

        [JsonProperty("NOTES4")]
        public string NOTES4 { get; set; }

        [JsonProperty("CURRSEL_TOTALS")]
        public int CURRSEL_TOTALS { get; set; }

        [JsonProperty("CURRSEL_DETAILS")]
        public int CURRSEL_DETAILS { get; set; }

        [JsonProperty("TRANSACTIONS")]
        public ArpSlipsTransactions TRANSACTIONS { get; set; }

        [JsonProperty("TIME")]
        public int TIME { get; set; }

        [JsonProperty("PROJECT_CODE")]
        public string PROJECT_CODE { get; set; }

        [JsonProperty("POS_TERMINAL_NR")]
        public string POS_TERMINAL_NR { get; set; }

        [JsonProperty("SALESMAN_CODE")]
        public string SALESMAN_CODE { get; set; }
    }
    public class ArpSlipsTransactions
    {
        public ArpSlipsTransactions()
        {
            items = new List<ArpSlipsTransaction>();
        }
        [JsonProperty("items")]
        public IList<ArpSlipsTransaction> items { get; set; }
    }
    public class ArpSlipsTransaction
    {

        [JsonProperty("INTERNAL_REFERENCE")]
        public object INTERNAL_REFERENCE { get; set; }

        [JsonProperty("ARP_CODE")]
        public string ARP_CODE { get; set; }
        [JsonProperty("GL_CODE1")]
        public string GL_CODE1 { get; set; }
        [JsonProperty("GL_CODE2")]
        public string GL_CODE2 { get; set; }

        [JsonProperty("DATE")]
        public DateTime DATE { get; set; }

        [JsonProperty("DEPARTMENT")]
        public int DEPARTMENT { get; set; }

        [JsonProperty("DIVISION")]
        public int DIVISION { get; set; }

        [JsonProperty("AUXIL_CODE")]
        public string AUXIL_CODE { get; set; }

        [JsonProperty("AUTH_CODE")]
        public string AUTH_CODE { get; set; }

        [JsonProperty("CYPHCODE")]
        public string CYPHCODE { get; set; }

        [JsonProperty("TRANNO")]
        public string TRANNO { get; set; }

        [JsonProperty("DOC_NUMBER")]
        public string DOC_NUMBER { get; set; }

        [JsonProperty("DESCRIPTION")]
        public string DESCRIPTION { get; set; }

        [JsonProperty("CREDIT")]
        public double CREDIT { get; set; }

        [JsonProperty("SIGN")]
        public int SIGN { get; set; }

        [JsonProperty("AMOUNT")]
        public double AMOUNT { get; set; }

        [JsonProperty("TC_XRATE")]
        public double TC_XRATE { get; set; }

        [JsonProperty("TC_AMOUNT")]
        public double TC_AMOUNT { get; set; }
        [JsonProperty("RC_AMOUNT")]
        public double RC_AMOUNT { get; set; }

        [JsonProperty("RC_XRATE")]
        public double RC_XRATE { get; set; }

        [JsonProperty("BNLN_TC_XRATE")]
        public double BNLN_TC_XRATE { get; set; }

        [JsonProperty("BNLN_TC_AMOUNT")]
        public double BNLN_TC_AMOUNT { get; set; }

        [JsonProperty("TRADING_GRP")]
        public string TRADING_GRP { get; set; }

        [JsonProperty("CURRSEL_TRANS")]
        public int CURRSEL_TRANS { get; set; }

        [JsonProperty("CREDIT_CARD_NO")]
        public string CREDIT_CARD_NO { get; set; }

        [JsonProperty("PROJECT_CODE")]
        public string PROJECT_CODE { get; set; }

        [JsonProperty("BATCH_NR")]
        public string BATCH_NR { get; set; }

        [JsonProperty("APPROVE_NR")]
        public string APPROVE_NR { get; set; }

        [JsonProperty("BANKACC_CODE")]
        public string BANKACC_CODE { get; set; }

        [JsonProperty("SALESMAN_CODE")]
        public string SALESMAN_CODE { get; set; }

        [Obsolete]
        [JsonProperty("AFFECT_RISK")] 
        public int AFFECT_RISK => 1;

    }




}
