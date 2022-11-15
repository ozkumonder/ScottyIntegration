using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottyIntegration.WebApi.Models.ERPModels
{
    public class GlSlip
    {
        public GlSlip()
        {
            TRANSACTIONS = new TRANSACTIONS();
        }
        public int TYPE { get; set; }
        public string NUMBER { get; set; }
        public DateTime DATE { get; set; }
        public string NOTES1 { get; set; }
        public string NOTES2 { get; set; }
        public string NOTES3 { get; set; }
        public string NOTES4 { get; set; }
        public TRANSACTIONS TRANSACTIONS { get; set; }
        public DateTime DOC_DATE { get; set; }
        public DateTime EBOOK_DOCDATE { get; set; }
        public string EBOOK_DOCNR { get; set; }
        public int EBOOK_DOCTYPE { get; set; }
        public int EBOOK_NOPAY { get; set; }
    }
    public class Item
    {
        public string GL_CODE { get; set; }
        public string OHP_CODE { get; set; }
        public double DEBIT { get; set; }
        public int LINENO { get; set; }
        public string DESCRIPTION { get; set; }
        public double RC_XRATE { get; set; }
        public double RC_AMOUNT { get; set; }
        public double TC_AMOUNT { get; set; }
        public DateTime DOC_DATE { get; set; }
        public int? SIGN { get; set; }
        public double? CREDIT { get; set; }
    }

    public class TRANSACTIONS
    {
        public TRANSACTIONS()
        {
            items = new List<Item>();
        }
        public IList<Item> items { get; set; }
    }


}
