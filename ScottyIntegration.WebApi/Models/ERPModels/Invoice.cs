using System;
using System.Collections.Generic;

namespace ScottyIntegration.WebApi.Models.ERPModels
{
    /// <summary>
    /// Fatura
    /// </summary>
    public class Invoice : BaseEntity
    {
        public Invoice()
        {
            TRANSACTIONS = new InvoiceTransactions();
        }
        /// <summary>
        /// Fatura Tipi 1: Mal alım faturası, 2: Perakende satış iade faturası, 3: Toptan satış iade faturası, 4: Alınan hizmet faturası, 5: Alınan proforma fatura, 6: alım iade faturası, 7: Perakende satış faturası, 8: Toptan satış faturası, 9: Verilen hizmet faturası, 10: Verilen proforma fatura, 13: Alınan Fiyat farkı faturası, 14: Verilen fiyat farkı faturası, 26: Müstahsil makbuzu
        /// </summary>
        public int? TYPE { get; set; }

        /// <summary>
        /// Fatura Numarası
        /// </summary>
        public string NUMBER { get; set; }
        /// <summary>
        /// Belge Numarası
        /// </summary>
        public string DOC_NUMBER { get; set; }
        /// <summary>
        /// Doküman İzleme Numarası 
        /// </summary>
        public string DOC_TRACK_NR { get; set; }
        /// <summary>
        /// Fatura Tarihi
        /// </summary>
        public DateTime DATE { get; set; }
        /// <summary>
        /// Fatura Saati
        /// </summary>
        public int? TIME { get; set; }
        /// <summary>
        /// Fatura Özel Kodu
        /// </summary>
        public string AUXIL_CODE { get; set; }
        /// <summary>
        /// Fatura Yetki Kodu
        /// </summary>
        public string AUTH_CODE { get; set; }
        public string GL_CODE { get; set; }
        public double? ADD_DISCOUNTS { get; set; }
        public double? TOTAL_DISCOUNTS { get; set; }
        public double? TOTAL_DISCOUNTED { get; set; }
        public double? ADD_EXPENSES { get; set; }
        public double? TOTAL_EXPENSES { get; set; }
        public double? TOTAL_VAT { get; set; }
        public double? TOTAL_GROSS { get; set; }
        public double? TOTAL_NET { get; set; }
        /// <summary>
        /// Fatura Açıklaması 1
        /// </summary>
        public string NOTES1 { get; set; }
        /// <summary>
        /// Fatura Açıklaması 2
        /// </summary>
        public string NOTES2 { get; set; }
        /// <summary>
        /// Fatura Açıklaması 3
        /// </summary>
        public string NOTES3 { get; set; }
        /// <summary>
        /// Fatura Açıklaması 4
        /// </summary>
        public string NOTES4 { get; set; }
        /// <summary>
        /// Fatura Açıklaması 5
        /// </summary>
        public string NOTES5 { get; set; }
        /// <summary>
        /// Fatura Açıklaması 6
        /// </summary>
        public string NOTES6 { get; set; }
        /// <summary>
        /// Cari Hesap Kodu
        /// </summary>
        public string ARP_CODE { get; set; }
        /// <summary>
        /// Masraf Merkezi Kodu
        /// </summary>
        public string OHP_CODE { get; set; }
        /// <summary>
        /// Proje Kodu
        /// </summary>
        public string PROJECT_CODE { get; set; }
        /// <summary>
        /// Fatura Kur Bilgisi 1 USD, 20 EUR
        /// </summary>
        public int? CURR_INVOICE { get; set; }
        /// <summary>
        /// İşlem Dövizi Kuru
        /// </summary>
        public double? TC_XRATE { get; set; }
        /// <summary>
        /// Kullanılacak Para Birimi Genel
        /// </summary>
        public int? CURRSEL_TOTALS { get; set; }
        /// <summary>
        /// Kullanılacak Para Birimi Satır
        /// </summary>
        public int? CURRSEL_DETAILS { get; set; }
        /// <summary>
        /// Satırlar
        /// </summary>
        public InvoiceTransactions TRANSACTIONS { get; set; }
        public PaymentList PAYMENT_LIST { get; set; }
        /// <summary>
        /// Satış İrsaliye yerine geçer
        /// </summary>
        public int? EINSTEAD_OF_DISPATCH { get; set; }
        public string SHIPPING_AGENT { get; set; }
        /// <summary>
        /// Düzenleme Tarihi
        /// </summary>
        public DateTime DOC_DATE { get; set; }
        /// <summary>
        /// E-Fatura Türü 1 E-Fatura, 2 E-Arşiv
        /// </summary>
        public int? EINVOICE { get; set; }
        /// <summary>
        /// E-Fatura Senaryo 1 Temel Fatura, 2 Ticari Fatura, 3 Yolcu Beraber Fatura
        /// </summary>
        /// <example>bla bla bla</example>
        /// <code>code nedur</code>
        /// <value>1</value>
        public int? PROFILE_ID { get; set; }
        /// <summary>
        /// Kdv Muafiyet Kodu
        /// </summary>
        public string VATEXCEPT_CODE { get; set; }
        /// <summary>
        /// E-Fatura - E-Arşiv Tipi 1 Özel Matrah, 2 İstisna, 3 Araç Tescil, 4 Tevkifat
        /// </summary>
        public int? EINVOICE_TYPE { get; set; }
        /// <summary>
        /// Satınalma İrsaliye yerine geçer
        /// </summary>
        [Obsolete]
        public int? EARCHIVEDETR_INSTEADOFDESP { get; set; }
        public int? EARCHIVEDETR_SENDMOD { get; set; }
        public int? EESTATUS { get; set; }
        public string ITEXT { get; set; }
        public int? ESTATUS { get; set; }
        public int? EBOOK_DOCTYPE { get; set; }

    }
    public class PaymentList
    {
        public PaymentList()
        {
            PAYMENT = new Payment();
        }
        public Payment PAYMENT { get; set; }
    }

    public class Payment
    {
        public DateTime DATE { get; set; }

        public double? TOTAL { get; set; }
        public DateTime PROCDATE { get; set; }

        public DateTime DISCOUNT_DUEDATE { get; set; }
    }
    /// <summary>
    /// Fatura Satırları
    /// </summary>
    public class InvoiceTransactions
    {
        public InvoiceTransactions()
        {
            items = new List<InvoiceTransaction>();
        }
        /// <summary>
        /// Fatura Satırları
        /// </summary>
        public List<InvoiceTransaction> items { get; set; }
    }
    /// <summary>
    /// Fatura Satırı
    /// </summary>
    public class InvoiceTransaction
    {
        /// <summary>
        /// Satır Tipi ;0 Malzeme satırı;1 Promosyon;2 İndirim;3 Masraf;4 Hizmet;5 Depozito;6 Karma koli satırı;7 Karma koli detayı;8 Sabit kıymet satırı
        /// </summary>
        public int? TYPE { get; set; }
        /// <summary>
        /// Malzeme Kodu
        /// </summary>
        public string MASTER_CODE { get; set; }
        /// <summary>
        /// Tarih
        /// </summary>
        public DateTime DATE { get; set; }
        /// <summary>
        /// Saat
        /// </summary>
        public int? FTIME { get; set; }
        /// <summary>
        /// Miktar
        /// </summary>
        public string GL_CODE1 { get; set; }
        public string GL_CODE2 { get; set; }
        public double? TOTAL { get; set; }
        public double? UNIT_CONV1 { get; set; }
        public double? UNIT_CONV2 { get; set; }
        public double? VAT_AMOUNT { get; set; }
        public double? VAT_BASE { get; set; }
        public int? VAT_INCLUDED { get; set; }
        public int? BILLED { get; set; }
        public double? TOTAL_NET { get; set; }
        public double? EXPENSE_DISTR { get; set; }
        public double? DISCOUNT_RATE { get; set; }
        public double? QUANTITY { get; set; }
        public double? PC_PRICE { get; set; }
        /// <summary>
        /// Birim Fiyat
        /// </summary>
        public double? PRICE { get; set; }
        /// <summary>
        /// Satır Kur Bilgisi 1 USD, 20 EUR
        /// </summary>
        public int? CURR_TRANSACTION { get; set; }
        public double? RC_XRATE { get; set; }
        public double? RC_NET { get; set; }
        public double? TC_NET { get; set; }
        /// <summary>
        /// İşlem Dövizi Kuru
        /// </summary>
        public double? TC_XRATE { get; set; }
        /// <summary>
        /// Birim Kodu
        /// </summary>
        public string UNIT_CODE { get; set; }
        /// <summary>
        /// İade Tipi
        /// </summary>
        public string RET_COST_TYPE { get; set; }
        /// <summary>
        /// Kdv Oranı
        /// </summary>
        public double? VAT_RATE { get; set; }
        /// <summary>
        /// Dövizli Para Türü
        /// </summary>
        public double? EDT_CURR { get; set; }
        /// <summary>
        /// Dövizli Birim Fiyat
        /// </summary>
        public double? EDT_PRICE { get; set; }
        /// <summary>
        /// Proje Kodu
        /// </summary>
        public string PROJECT_CODE { get; set; }
        /// <summary>
        /// Kdv Muafiyet Kodu
        /// </summary>
        public string VATEXCEPT_CODE { get; set; }
        /// <summary>
        /// Satır Açıklaması
        /// </summary>
        public string DESCRIPTION { get; set; }


    }

}