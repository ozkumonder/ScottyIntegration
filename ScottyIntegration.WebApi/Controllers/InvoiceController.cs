using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScottyIntegration.WebApi.Core.Helper;
using ScottyIntegration.WebApi.Manager;
using ScottyIntegration.WebApi.Models.Dtos;
using ScottyIntegration.WebApi.Models.ERPModels;
using ScottyIntegration.WebApi.Models.ResultTypes;

namespace ScottyIntegration.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InvoiceController : ControllerBase
    {
        private int postId = 0;
        private int accountPostId = 0;
        private readonly ILogger<InvoiceController> _logger;
        private readonly LogController _logController;
        private readonly ServiceManager serviceManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public InvoiceController(ILogger<InvoiceController> logger)
        {
            _logger = logger;
            serviceManager = new ServiceManager();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<ServiceResult> Invoice(RiderServiceModel model)
        {
            var httpContext = HttpContext;
            var clientServiceResult = new ServiceResult();
            var serviceResults = new List<ServiceResult>();
            try
            {
                #region Cari İşlemleri
                var clientCard = new ClientCard
                {
                    //CODE = "~",
                    TITLE = model.ClientCommercial,
                    ADDRESS1 = model.ClientInvoiceAddress,
                    NAME = model.ClientName,
                    SURNAME = model.ClientSurName,
                    E_MAIL = model.ClientEMail,
                    E_COMM_ID = model.ClientId.ToString()
                };
                if (model.ClientTaxNumber.Length == 10)
                {
                    clientCard.TAX_ID = model.ClientTaxNumber;
                }
                if (model.ClientTaxNumber.Length == 11)
                {
                    clientCard.TCKNO = model.ClientTaxNumber;
                    clientCard.PERSCOMPANY = 1;
                }
                clientServiceResult = serviceManager.AddClient(clientCard, httpContext);
                serviceResults.Add(clientServiceResult);
                #endregion
                if (clientServiceResult.ObjectNo != null)
                {
                    #region Fatura İşlemi
                    var serviceInvoice = new Invoice
                    {
                        TYPE = 9,
                        NUMBER = "~",
                        DATE = model.PaymentCreatedDate,
                        DOC_DATE = model.PaymentCreatedDate,
                        ARP_CODE = clientServiceResult.ObjectNo.ToString(),
                        NOTES1 = model.Notes,
                        CURRSEL_TOTALS = 1
                    };
                    var serviceInvoiceTransaction = new InvoiceTransaction
                    {
                        TYPE = 4,
                        MASTER_CODE = model.ServiceCode,
                        QUANTITY = 1,
                        PRICE = model.Price,
                        UNIT_CODE = "ADET",
                        VAT_RATE = 18,
                        VAT_INCLUDED = 1
                    };
                    serviceInvoice.TRANSACTIONS.items.Add(serviceInvoiceTransaction);
                    var invoiceServiceResult = serviceManager.SalesInvoice(serviceInvoice, httpContext);
                    serviceResults.Add(invoiceServiceResult);
                    #endregion

                    #region Alacak Dekontu
                    var arpSlip = new ArpSlip
                    {
                        TYPE = 4,
                        NUMBER = "~",
                        DATE = DateTime.Now,
                        NOTES1 = "Açıklama 1",
                        NOTES2 = "Açıklama 2",
                        NOTES3 = "Açıklama 3",
                        NOTES4 = "Açıklama 4",

                    };
                    var arpSlipTrancastion = new ArpSlipsTransaction
                    {
                        ARP_CODE = "M1201.0001", //serviceResult.ObjectNo.ToString()??"~",
                        TRANNO = "~",
                        CREDIT = model.Price,
                        GL_CODE2 = "108.01.1.001",
                        //SIGN = 1,
                        AMOUNT = model.Price,
                        TC_XRATE = 1,
                        TC_AMOUNT = model.Price,
                        RC_XRATE = 1,
                        RC_AMOUNT = model.Price,
                        //BNLN_TC_XRATE = 1,
                        //BNLN_TC_AMOUNT = model.Price,
                        //BANKACC_CODE = "B.002512345",

                    };
                    var arpSlipTrancastions = new List<ArpSlipsTransaction>
                    {
                        arpSlipTrancastion
                    };
                    arpSlip.TRANSACTIONS.items.Add(arpSlipTrancastion);
                    var arpServiceResult = new ServiceResult(); //serviceManager.ArpSlip(arpSlip, httpContext);
                    serviceResults.Add(arpServiceResult);
                    #endregion

                    #region Mahsup İşlemi

                    

                    #endregion
                }
                else
                {
                    LogHelper.Log("Cari kodu boş olduğundan bir sonraki işleme devam edilecemeyektir!");
                }

            }
            catch (Exception e)
            {
                LogHelper.Log(string.Concat(LogHelper.LogType.Error.ToLogType(), " ", e.Message));
            }


            //var arpSlipResult = 
            return serviceResults;
        }
        //public JToken AddClient(ClientCard clientCard)
        //{
        //    var request = HttpContext.Request;
        //    var tokenHolder = TokenHolder.GetInstance();
        //    JToken jResult = string.Empty;
        //    var restService = new TigerRestService();
        //    var isSend = false;
        //    string accountingCode = string.Empty;
        //    string result = string.Empty;
        //    string json = string.Empty;

        //    var requestDto = new RequestDto
        //    {
        //        RequestDate = DateTime.Now,
        //        RequestIp = request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
        //        UserIdentity = this.User.Identity.Name,
        //        RequestUrl = request.Path,
        //        RequestMethod = request.Method,
        //        RequestData = json
        //    };
        //    if (clientCard != null)
        //    {
        //        try
        //        {
        //            //clientCard.DataObjectParameter = new DataObjectParameter();
        //            json = JsonConvert.SerializeObject(clientCard, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //            postId = _logController.InsertPostLog(requestDto);
        //            if (!string.IsNullOrEmpty(clientCard.CODE))
        //            {
        //                if (restService.GetAccessToken().IsLoggedIn)
        //                {
        //                    if (!DataLogic.CheckClCard(clientCard.CODE))
        //                    {
        //                        #region Muhasebe Hesabı Kontrolu
        //                        //if (!DataLogic.CheckEmuAccCode(clientCard.CODE))
        //                        //{
        //                        //    var muhCode = string.Empty;
        //                        //    var split = clientCard.CODE.Split('.');
        //                        //    bool first = true;
        //                        //    foreach (var s in split)
        //                        //    {
        //                        //        string code = null;
        //                        //        code = muhCode != "" ? muhCode += s : muhCode;
        //                        //        accountingCode = code == "" ? s : muhCode;
        //                        //        if (!DataLogic.CheckEmuAccCode(accountingCode))
        //                        //        {
        //                        //            var muhCode1 = new EmuhAcc
        //                        //            {
        //                        //                CODE = accountingCode,
        //                        //                DESCRIPTION = clientCard.TITLE,
        //                        //                ACCOUNT_TYPE = 2,
        //                        //                FTFLAGS = "000000000000"
        //                        //            };
        //                        //            var jsonEmuhBase = JsonConvert.SerializeObject(muhCode1, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //                        //            accountPostId = _logController.InsertPostLog(DateTime.Now, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, "/IntegrationRestApi/api/GLAccounts", string.Concat(request.Path, "/api/GLAccounts"), request.Method, jsonEmuhBase);
        //                        //            jResult = restService.HttpPost(TigerDataType.GLAccounts.Value, jsonEmuhBase);
        //                        //            _logController.InsertResponseLog(accountPostId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, "/IntegrationRestApi/api/GLAccounts", jsonEmuhBase, jResult.ToString());
        //                        //        }
        //                        //        if (first)
        //                        //        {
        //                        //            muhCode += string.Concat(s, ".");
        //                        //            first = false;
        //                        //        }
        //                        //        else
        //                        //        {
        //                        //            muhCode += ".";
        //                        //        }
        //                        //    }

        //                        //    if (DataLogic.CheckEmuAccCode(clientCard.CODE))
        //                        //    {
        //                        //        clientCard.GL_CODE = clientCard.CODE;
        //                        //    }
        //                        //}
        //                        //else
        //                        //{
        //                        //    accountingCode = clientCard.CODE;
        //                        //}
        //                        #endregion

        //                        #region Firma Kontrolü

        //                        if (clientCard.ISFOREIGN != 1)
        //                        {
        //                            if (clientCard.PERSCOMPANY == 1 && !string.IsNullOrEmpty(clientCard.TCKNO))
        //                            {
        //                                if (!string.IsNullOrEmpty(clientCard.NAME) && !string.IsNullOrEmpty(clientCard.SURNAME))
        //                                {
        //                                    if (clientCard.TCKNO.Length == 11)
        //                                    {
        //                                        isSend = true;
        //                                    }
        //                                    else
        //                                    {
        //                                        isSend = false;
        //                                        jResult = string.Concat(clientCard.TCKNO, @" Girilen TC Kimlik numarası 11 karakter olmalıdır!");
        //                                        _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, json, jResult.ToString());
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    isSend = false;
        //                                    jResult = @"Şahıs firması için adı soyadı bilgisi zorunludur.";
        //                                    _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
        //                                        this.User.Identity.Name, request.Path, json,
        //                                        jResult.ToString());
        //                                }
        //                                if (string.IsNullOrEmpty(clientCard.E_MAIL))
        //                                {
        //                                    isSend = false;
        //                                    jResult = @"Şahıs firması için eposta bilgisi bilgisi zorunludur.";
        //                                    _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
        //                                        this.User.Identity.Name, request.Path, json,
        //                                        jResult.ToString());
        //                                }
        //                            }
        //                            else if ((clientCard.PERSCOMPANY == 0 || clientCard.PERSCOMPANY == null) && !string.IsNullOrEmpty(clientCard.TAX_ID))
        //                            {
        //                                if (clientCard.TAX_ID.Length == 10)
        //                                {
        //                                    var gibInfo = ConnectService.CheckGibUser(clientCard.TAX_ID);
        //                                    if (!gibInfo.IsGibUser)
        //                                    {
        //                                        isSend = false;
        //                                        jResult = string.Concat(clientCard.TAX_ID, @" ilgili vergi kimlik numarası doğrulanamadı. Lütfen geçerli bir vergi kimlik numarası giriniz!");
        //                                    }
        //                                    else
        //                                    {
        //                                        clientCard.ACCEPT_EINV = gibInfo.IsGibUser.ToByte();
        //                                        clientCard.POST_LABEL = gibInfo.PkLabel;
        //                                        clientCard.SENDER_LABEL = gibInfo.GbLabel;
        //                                        isSend = true;
        //                                    }

        //                                }
        //                                else
        //                                {
        //                                    isSend = false;
        //                                    jResult = string.Concat(clientCard.TAX_ID, @" Girilen Vergi Kimlik numarası 10 karakter olmalıdır!");
        //                                    _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
        //                                        this.User.Identity.Name, request.Path, json,
        //                                        jResult.ToString());
        //                                }
        //                            }
        //                            else
        //                            {
        //                                isSend = false;
        //                                jResult = "Vergi Kimlik veya TC Kimlik numarası boş geçilemez!";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            isSend = true;
        //                        }

        //                        #endregion
        //                        //else
        //                        //{
        //                        //    isSend = false;
        //                        //    jResult = "İlgili Cari Hesap Kartı logo'da mevcut olduğundan gönderilemedi!";
        //                        //}
        //                    }
        //                    else
        //                    {
        //                        isSend = false;
        //                        jResult = string.Concat(DataLogic.GetClientRefByCode(clientCard.CODE), " - ", clientCard.CODE, " ", clientCard.TITLE, " İlgili Cari Hesap Kartı logo'da mevcut olduğundan gönderilemedi!");
        //                        _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, json, jResult.ToString());
        //                    }
        //                    if (isSend)
        //                    {
        //                        //clientCard.GL_CODE = accountingCode;
        //                        json = JsonConvert.SerializeObject(clientCard, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //                        jResult = restService.HttpPost(TigerDataType.Arps.Value, json);
        //                        if (jResult["INTERNAL_REFERENCE"].ToObject<int>() > 0)
        //                        {
        //                            _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, json, jResult.ToString());
        //                        }
        //                        else
        //                        {
        //                            _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, json, jResult.ToString());

        //                            _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, jResult.ToString(), json, jResult.ToString());
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    jResult = string.Concat(@"Token alma başarısız!", tokenHolder.Token);
        //                    _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, jResult.ToString(), json, jResult.ToString());
        //                }
        //            }
        //            else
        //            {
        //                jResult = "Cari kartı kodunu boş gönderemezsiniz";
        //                _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, json, jResult.ToString());
        //                _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, jResult.ToString(), json, jResult.ToString());
        //            }

        //        }
        //        catch (Exception exception)
        //        {
        //            LogHelper.LogError(string.Concat("ERROR: ", typeof(InvoiceController), MethodBase.GetCurrentMethod().Name, exception.Message));
        //            _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, json, jResult.ToString());
        //            _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, exception.Message, json, jResult.ToString());
        //            jResult = exception.Message;
        //        }
        //    }
        //    else
        //    {
        //        jResult = "Göndermiş olduğunuz clientCard parametresi hatalıdır!";
        //        _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, json, jResult.ToString());
        //        _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), this.User.Identity.Name, request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, jResult.ToString(), json, jResult.ToString());
        //    }
        //    return jResult;
        //}
    }
}
