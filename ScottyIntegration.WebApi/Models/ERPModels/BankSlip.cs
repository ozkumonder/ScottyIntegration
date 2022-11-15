using System;
using System.Collections.Generic;

namespace ScottyIntegration.WebApi.Models.ERPModels
{
    /// <summary>
    /// Banka Hareketleri
    /// </summary>
    public class BankSlip : BaseEntity
    {
        public BankSlip()
        {
            TRANSACTIONS = new BankSlipTransactions();
        }
        /// <summary>
        /// Kayıt Referansı
        /// </summary>
        public int? INTERNAL_REFERENCE { get; set; }

        /// <summary>
        /// Fiş Tipi
        /// </summary>
        public int? TYPE { get; set; }
        /// <summary>
        /// Tarih
        /// </summary>
        public DateTime? DATE { get; set; }
        /// <summary>
        /// Fiş No "~" olarak gönderildiğinde logo numaralama şablonunun ürettiği numarayı alır
        /// </summary>
        public string NUMBER { get; set; }
        /// <summary>
        /// Özel Kodu
        /// </summary>
        public string AUXIL_CODE { get; set; }
        /// <summary>
        /// Yetki Kodu
        /// </summary>
        public string AUTH_CODE { get; set; }
        /// <summary>
        /// İşyeri
        /// </summary>
        public int? DIVISION { get; set; }
        /// <summary>
        /// Bölüm
        /// </summary>
        public int? DEPARMENT { get; set; }
        /// <summary>
        /// Borç - Alacak İşareti - 0 Borç 1 Alacak
        /// </summary>
        public int? SIGN { get; set; }
        /// <summary>
        /// Açıklama
        /// </summary>
        public string NOTES1 { get; set; }
        /// <summary>
        /// Açıklama
        /// </summary>
        public string NOTES2 { get; set; }
        /// <summary>
        /// Kullanılacak Para Birimi Genel - 0 Raporlama Dövizi
        /// </summary>
        public int? CURRSEL_TOTALS { get; set; }
        /// <summary>
        /// Kullanılacak Para Birimi Satır - 0 Yerel Para Birimi, 2 İşlem Dövizi
        /// </summary>
        public int? CURRSEL_DETAILS { get; set; }

        public BankSlipTransactions TRANSACTIONS { get; set; }
        [Obsolete]
        public int EBOOK_DOCTYPE { get; set; }
        [Obsolete]
        public int BNCREREF { get; set; }
        [Obsolete]
        public string BANK_CREDIT_CODE { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class BankSlipTransaction
    {
        /// <summary>
        /// Satır Tipi - 3 Gelen Havale, 4 Gönderilen Havale
        /// </summary>
        public int? TYPE { get; set; }
        /// <summary>
        /// İşlem No
        /// </summary>
        public string TRANNO { get; set; }
        /// <summary>
        /// Banka Hesap Kodu
        /// </summary>
        public string BANKACC_CODE { get; set; }
        /// <summary>
        /// Cari Hesap Kodu
        /// </summary>
        public string ARP_CODE { get; set; }
        /// <summary>
        /// Tarih
        /// </summary>
        public DateTime? DATE { get; set; }
        /// <summary>
        /// Borç - Alacak İşareti - 0 Borç 1 Alacak
        /// </summary>
        public int? SIGN { get; set; }
        /// <summary>
        /// Hareket Özel Kodu 
        /// </summary>
        public string AUXIL_CODE { get; set; }
        /// <summary>
        /// Belge No
        /// </summary>
        public string DOC_NUMBER { get; set; }
        /// <summary>
        /// Muhasebe Kodu 1
        /// </summary>
        public string GL_CODE1 { get; set; }
        /// <summary>
        /// Muhasebe Kodu 2
        /// </summary>
        public string GL_CODE2 { get; set; }
        /// <summary>
        /// Satır Açıklaması
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// İşlem Dövizi 0 TL, 1 USD, 17 GBP, 20 EUR
        /// </summary>
        public int? CURR_TRANS { get; set; }
        /// <summary>
        /// Borç
        /// </summary>
        public decimal? DEBIT { get; set; }
        /// <summary>
        /// Alacak
        /// </summary>
        public decimal? CREDIT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Obsolete]
        public decimal? AMOUNT { get; set; }
        /// <summary>
        /// İşlem Dövizi Kuru 
        /// </summary>
        public decimal? TC_XRATE { get; set; }
        /// <summary>
        /// İşlem Dövizi Tutarı
        /// </summary>
        public decimal? TC_AMOUNT { get; set; }
        /// <summary>
        /// Raporlama Dövizi Kuru
        /// </summary>
        public decimal? RC_XRATE { get; set; }
        /// <summary>
        /// Raporlama Dövizi Tutarı
        /// </summary>
        public decimal? RC_AMOUNT { get; set; }
        /// <summary>
        /// CH Banka Şubesi
        /// </summary>
        public string ARP_BNKDIV_NR { get; set; }
        /// <summary>
        /// CH Banka Hesap No
        /// </summary>
        public string ARP_BNKACCOUNT_NR { get; set; }
        /// <summary>
        /// Banka Takip No
        /// </summary>
        public string BNK_TRACKING_NR { get; set; }
        /// <summary>
        /// Ticari İşlem Grubu
        /// </summary>
        public string TRADING_GRP { get; set; }
        /// <summary>
        /// Satır Döviz Türü  0 TL, 1 USD, 17 GBP, 20 EUR
        /// </summary>
        public int? CURRSEL_TRANS { get; set; }
        /// <summary>
        /// Banka Hareket Türü - 2 Havale 3 EFT
        /// </summary>
        public int? BANK_PROC_TYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DUE_DATE { get; set; }
        /// <summary>
        /// Proje Kodu
        /// </summary>
        public string PROJECT_CODE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Obsolete]
        public byte AFFECT_RISK { get; set; }
        /// <summary>
        /// BI Kodu
        /// </summary>
        [Obsolete]
        public string BANK_BRANCHS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Obsolete]
        public string SALESMAN_CODE { get; set; }
        /// <summary>
        /// IBAN
        /// </summary>
        [Obsolete]
        public string IBAN { get; set; }
        [Obsolete]
        public int? BN_CRDTYPE { get; set; }
        [Obsolete]
        public int? DIVISION { get; set; }
    }

    public class BankSlipTransactions
    {
        public BankSlipTransactions()
        {
            items = new List<BankSlipTransaction>();
        }
        public List<BankSlipTransaction> items { get; set; }
    }

}
