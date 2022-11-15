using System;
using System.ComponentModel.DataAnnotations;
using ConnectPostbox;
using Newtonsoft.Json;

namespace ScottyIntegration.WebApi.Models.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class RiderServiceModel
    {
        #region Client Properties
        /// <summary>
        /// Müşteri Id
        /// </summary>
        [JsonProperty("ClientId")]
        public int ClientId { get; set; }
        /// <summary>
        /// Müşteri Adı
        /// </summary>
        [JsonProperty("ClientName")]
        public string ClientName { get; set; }
        /// <summary>
        /// Müşteri Soyadı
        /// </summary>
        [JsonProperty("ClientSurName")]
        public string ClientSurName { get; set; }
        /// <summary>
        /// Müşteri Ticari Unvan veya İsim Soyisim
        /// </summary>
        [JsonProperty("ClientCommercial")]
        public string ClientCommercial { get; set; }
        /// <summary>
        /// Müşteri Fatura Adresi
        /// </summary>
        [JsonProperty("ClientInvoiceAddress")]
        public string ClientInvoiceAddress { get; set; }
        /// <summary>
        /// Cari Mail Adresi
        /// </summary>
        [JsonProperty("ClientEMail")]
        public string ClientEMail { get; set; }
        /// <summary>
        /// Müşteri Vergi Kimlik No - TC Kimlik No
        /// </summary>
        [JsonProperty("ClientTaxNumber")]
        public string ClientTaxNumber { get; set; }
        #endregion

        #region Payment Properties

        /// <summary>
        /// Ödeme Id
        /// </summary>
        [JsonProperty("PaymentId")]
        public int PaymentId { get; set; }
        /// <summary>
        /// Ödeme Oluşturma Tarihi
        /// </summary>
        [JsonProperty("PaymentCreatedDate")]
        public DateTime PaymentCreatedDate { get; set; }
        /// <summary>
        /// Fiyat
        /// </summary>
        [JsonProperty("Price")]
        public double Price { get; set; }
        /// <summary>
        /// Komisyon Oranı
        /// </summary>
        [JsonProperty("ProviderCommissionFee")]
        public double ProviderCommissionFee { get; set; }
        /// <summary>
        /// Komisyon Miktarı
        /// </summary>
        [JsonProperty("ProviderCommissionAmount")]
        public double ProviderCommissionAmount { get; set; }
        /// <summary>
        /// Ödeme Id (provider_payment_id)
        /// </summary>
        [JsonProperty("ProviderPaymentId")]
        public int ProviderPaymentId { get; set; }
        /// <summary>
        /// Ödeme Transaction Id
        /// </summary>
        [JsonProperty("ProviderTransactionId")]
        public int ProviderTransactionId { get; set; }
        /// <summary>
        /// Trip Id
        /// </summary>
        [JsonProperty("TripId")]
        public int TripId { get; set; }
        #endregion

        #region Invoice Properties
        /// <summary>
        /// Fatura Açıklaması
        /// </summary>
        [StringLength(200)]
        public string Notes { get; set; }
        /// <summary>
        /// Hizmet Kodu
        /// </summary>
        public string ServiceCode { get; set; }

        #endregion
    }
}
