using System;

namespace ScottyIntegration.WebApi.Models.ERPModels
{
    /// <summary>
    /// Cari Hesap Kartı
    /// </summary>
    public class ClientCard
    {
        private int? _salesbrws;
        private int? _purchbrws;
        private int? _impbrws;
        private int? _expbrws;
        private int? _finbrws;
        private int? _collatrlriskType;
        private int? _riskType1;
        private int? _riskType2;
        private int? _riskType3;
        private int? _ordDay;
        private int? _profileidDesp;
        private int? _correspLang;
        private int? _clOrdFrequest;
        private int? _eInv_eArcType;


        /// <summary>
        /// Logo Kayıt Id Bilgisi
        /// </summary>
        [Obsolete]
        public int? INTERNAL_REFERENCE { get; set; }
        /// <summary>
        /// Cari Hesap Tipi 1 Alıcı, 2 Satıcı, 3 Alıcı Satıcı, 4 Grup Şirketi
        /// </summary>
        public int? ACCOUNT_TYPE { get; set; }
        /// <summary>
        /// Cari Hesap Kodu
        /// </summary>
        public string CODE { get; set; }
        /// <summary>
        /// Cari Hesap Unvanı
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// Cari Hesap Unvanı 2
        /// </summary>
        public string TITLE2 { get; set; }
        /// <summary>
        /// E-İş Kodu
        /// </summary>
        public string LOGOID { get; set; }
        /// <summary>
        /// Fatura Yazım Sayısı
        /// </summary>
        public int INVOICE_PRNT_CNT { get; set; }
        /// <summary>
        /// Adres 1
        /// </summary>
        public string ADDRESS1 { get; set; }
        /// <summary>
        /// Adres 2
        /// </summary>
        public string ADDRESS2 { get; set; }
        /// <summary>
        /// İlçe Kodu Örn; 255
        /// </summary>
        public string TOWN_CODE { get; set; }
        /// <summary>
        /// İlçe
        /// </summary>
        public string TOWN { get; set; }
        /// <summary>
        /// İl Kodu Örn; 34
        /// </summary>
        public string CITY_CODE { get; set; }
        /// <summary>
        /// İl
        /// </summary>
        public string CITY { get; set; }
        /// <summary>
        /// Ülke Kodu
        /// </summary>
        public string COUNTRY_CODE { get; set; }
        /// <summary>
        /// Ülke
        /// </summary>
        public string COUNTRY { get; set; }
        /// <summary>
        /// Posta Kodu
        /// </summary>
        public string POSTAL_CODE { get; set; }
        /// <summary>
        /// Telefon Alan Kodu 1
        /// </summary>
        public string TELEPHONE1_CODE { get; set; }
        /// <summary>
        /// Telefon 1
        /// </summary>
        public string TELEPHONE1 { get; set; }
        /// <summary>
        /// Telefon Alan Kodu 2
        /// </summary>
        public string TELEPHONE2_CODE { get; set; }
        /// <summary>
        /// Telefon 2
        /// </summary>
        public string TELEPHONE2 { get; set; }
        /// <summary>
        /// Cari Hesap Döviz Türü O:TL -  1:USD - 20:EUR - 17:GBP
        /// </summary>
        public short CURRENCY { get; set; }
        /// <summary>
        /// Vergi Kimlik No
        /// </summary>
        public string TAX_ID { get; set; }
        /// <summary>
        /// Şahıs Şirketi
        /// </summary>
        public byte? PERSCOMPANY { get; set; }
        /// <summary>
        /// Yabancı Uyruk
        /// </summary>
        public short? ISFOREIGN { get; set; }
        /// <summary>
        /// TC Kimlik No
        /// </summary>
        public string TCKNO { get; set; }
        /// <summary>
        /// Adı
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// Soyadı
        /// </summary>
        public string SURNAME { get; set; }
        /// <summary>
        /// Vergi Dairesi
        /// </summary>
        public string TAX_OFFICE { get; set; }
        /// <summary>
        /// Vergi Dairesi Kodu
        /// </summary>
        public string TAX_OFFICE_CODE { get; set; }
        /// <summary>
        /// E-Posta
        /// </summary>
        public string E_MAIL { get; set; }
        /// <summary>
        /// E-Posta 2
        /// </summary>
        public string E_MAIL2 { get; set; }
        /// <summary>
        /// E-Posta 3
        /// </summary>
        public string E_MAIL3 { get; set; }
        /// <summary>
        /// İlgili
        /// </summary>
        public string CONTACT { get; set; }
        /// <summary>
        /// İlgili
        /// </summary>
        public string CONTACT2 { get; set; }
        /// <summary>
        /// İlgili
        /// </summary>
        public string CONTACT3 { get; set; }
        /// <summary>
        /// Yazışma Dili (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? CORRESP_LANG
        {
            get => _correspLang == 0 ? 1 : _correspLang ?? 1;
            set => _correspLang = value;
        }
        /// <summary>
        /// Risk Faktörü Çek Kendi 
        /// </summary>
        public double RISKFACT_CHQ { get; set; }
        /// <summary>
        /// Risk Faktörü Senet Kendi 
        /// </summary>
        public double RISKFACT_PROMNT { get; set; }
        /// <summary>
        /// Muhasebe Hesap Kodu
        /// </summary>
        public string GL_CODE { get; set; }
        /// <summary>
        /// Masraf Merkezi Kodu
        /// </summary>
        public string OHP_CODE { get; set; }
        /// <summary>
        /// Proje Kodu
        /// </summary>
        public string PROJECT_CODE { get; set; }

        /// <summary>
        /// Sipariş Sıklığı (Gün) (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? CL_ORD_FREQ
        {
            get => _clOrdFrequest == 0 ? 1 : _clOrdFrequest ?? 1;
            set => _clOrdFrequest = value;
        }
        /// <summary>
        /// Sipariş Günleri (Default Value = 127 )
        /// </summary>
        [Obsolete]
        public int? ORD_DAY
        {
            get => _ordDay == 0 ? 127 : _ordDay ?? 127;
            set => _ordDay = value;
        }
        /// <summary>
        /// Kullanım Yeri Satınalma (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? PURCHBRWS
        {
            get => _purchbrws == 0 ? 1 : _purchbrws ?? 1;
            set => _purchbrws = value;
        }
        /// <summary>
        /// Kullanım Yeri Satış (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? SALESBRWS
        {
            get => _salesbrws == 0 ? 1 : _salesbrws ?? 1;
            set => _salesbrws = value;
        }
        /// <summary>
        /// Kullanım Yeri İhtalat (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? IMPBRWS
        {
            get => _impbrws == 0 ? 1 : _impbrws ?? 1;
            set => _impbrws = value;
        }
        /// <summary>
        /// Kullanım Yeri İhracat (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? EXPBRWS
        {
            get => _expbrws == 0 ? 1 : _expbrws ?? 1;
            set => _expbrws = value;
        }
        /// <summary>
        /// Kullanım Yeri Finans (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? FINBRWS
        {
            get => _finbrws == 0 ? 1 : _finbrws ?? 1;
            set => _finbrws = value;
        }
        /// <summary>
        /// COLLATRLRISK_TYPE (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? COLLATRLRISK_TYPE
        {
            get => _collatrlriskType == 0 ? 1 : _collatrlriskType ?? 1;
            set => _collatrlriskType = value;
        }
        /// <summary>
        /// RISK_TYPE1 (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? RISK_TYPE1
        {
            get => _riskType1 == 0 ? 1 : _riskType1 ?? 1;
            set => _riskType1 = value;
        }
        /// <summary>
        /// RISK_TYPE2 (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? RISK_TYPE2
        {
            get => _riskType2 == 0 ? 1 : _riskType2 ?? 1;
            set => _riskType2 = value;
        }
        /// <summary>
        /// RISK_TYPE3 (Default Value = 1 )
        /// </summary>
        [Obsolete]
        public int? RISK_TYPE3
        {
            get => _riskType3 == 0 ? 1 : _riskType3 ?? 1;
            set => _riskType3 = value;
        }
        /// <summary>
        /// Risk Faktörü Çek Müşteri 
        /// </summary>
        public double CST_CEK_RISK_FACTOR { get; set; }
        /// <summary>
        /// Risk Faktörü Senet Müşteri 
        /// </summary>
        public double CST_SENET_RISK_FACTOR { get; set; }
        /// <summary>
        /// E-Fatura Senaryo 0 Temel Fatura, 1 Ticari Fatura, 2 Yolcu Beraber Fatura
        /// </summary>
        [Obsolete]
        public int? PROFILE_ID { get; set; }
        /// <summary>
        /// Risk Faktörü Çek Ciro
        /// </summary>
        public double CST_CIRO_CEK_RISK_FAC { get; set; }
        /// <summary>
        /// Risk Faktörü Senet Ciro
        /// </summary>
        public double CST_CIRO_SENET_RISK_FAC { get; set; }
        /// <summary>
        /// E-Arşiv Gönderim Şekli 1 Kağıt, 2 Elektronik
        /// </summary>
        [Obsolete]
        public int? EARCHIVE_SEND_MODE { get; set; }
        /// <summary>
        /// E-İrsaliye Senaryo (Default Value = 1 )
        /// </summary>
        public int? PROFILEID_DESP
        {
            get => _profileidDesp == 0 ? 1 : _profileidDesp ?? 1;
            set => _profileidDesp = value;
        }
        /// <summary>
        /// Veri Aktarım No
        /// </summary>
        public string E_COMM_ID { get; set; }
        /// <summary>
        /// Posta Kutusu Etiketi
        /// </summary>
        [Obsolete]
        public string POST_LABEL { get; set; }
        /// <summary>
        /// Göderici Birim Etiketi  
        /// </summary>
        [Obsolete]
        public string SENDER_LABEL { get; set; }
        /// <summary>
        /// E-Fatura Kullanıcısı
        /// </summary>
        [Obsolete]
        public byte? ACCEPT_EINV { get; set; }
        /// <summary>
        /// E-Fatura/E-Arşiv Tipi
        /// </summary>
        [Obsolete]
        public int? EINV_EARC_TYPE
        {
            get => _eInv_eArcType == 0 ? 2 : _eInv_eArcType ?? 2;
            set => _eInv_eArcType = value;
        }

    }
}