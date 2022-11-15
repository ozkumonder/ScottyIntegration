using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScottyIntegration.WebApi.Controllers;
using ScottyIntegration.WebApi.Core.DataAccess;
using ScottyIntegration.WebApi.Core.Helper;
using ScottyIntegration.WebApi.Core.LogoRestIntegretion;
using ScottyIntegration.WebApi.Core.Utilities;
using ScottyIntegration.WebApi.Models.Dtos;
using ScottyIntegration.WebApi.Models.ERPModels;
using ScottyIntegration.WebApi.Models.Global;
using ScottyIntegration.WebApi.Models.ResultTypes;

namespace ScottyIntegration.WebApi.Manager
{
    public class ServiceManager : ControllerBase
    {
        private int postId = 0;
        private int accountPostId = 0;
        private LogController _logController = new LogController();
        public ServiceManager()
        {
            //_logController = new LogController();
        }
        public ServiceResult AddClient(ClientCard clientCard, HttpContext httpContext)
        {
            var serviceResult = new ServiceResult();
            var request = httpContext.Request;
            var tokenHolder = TokenHolder.GetInstance();
            JToken jResult = string.Empty;
            var restService = new TigerRestService();
            var isSend = false;
            string accountingCode = string.Empty;
            string result = string.Empty;
            string json = string.Empty;

            var requestDto = new RequestDto
            {
                RequestDate = DateTime.Now,
                RequestIp = request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
                UserIdentity = "",
                RequestUrl = request.Path,
                RequestMethod = request.Method,
                RequestData = json
            };
            if (clientCard != null)
            {
                var clientInfo = new ClientInfoDto();
                try
                {
                    if (!string.IsNullOrEmpty(clientCard.TAX_ID))
                    {
                        clientInfo.TaxNr = clientCard.TAX_ID;
                    }
                    else if (!string.IsNullOrEmpty(clientCard.TCKNO))
                    {
                        clientInfo.TaxNr = clientCard.TCKNO;
                    }

                    clientInfo = DataLogic.CheckClientWithTaxOrTckn(clientInfo.TaxNr);
                    clientCard.CODE = clientInfo.CheckingResult ? clientInfo.Code : DataLogic.GetLastClientCode();
                    //clientCard.DataObjectParameter = new DataObjectParameter();

                    json = JsonConvert.SerializeObject(clientCard, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    postId = _logController.InsertPostLog(requestDto);

                    if (!string.IsNullOrEmpty(clientCard.CODE))
                    {
                        if (restService.GetAccessToken().IsLoggedIn)
                        {
                            
                            if (!clientInfo.CheckingResult)
                            {
                                
                                #region Muhasebe Hesabı Kontrolu
                                //if (!DataLogic.CheckEmuAccCode(clientCard.CODE))
                                //{
                                //    var muhCode = string.Empty;
                                //    var split = clientCard.CODE.Split('.');
                                //    bool first = true;
                                //    foreach (var s in split)
                                //    {
                                //        string code = null;
                                //        code = muhCode != "" ? muhCode += s : muhCode;
                                //        accountingCode = code == "" ? s : muhCode;
                                //        if (!DataLogic.CheckEmuAccCode(accountingCode))
                                //        {
                                //            var muhCode1 = new EmuhAcc
                                //            {
                                //                CODE = accountingCode,
                                //                DESCRIPTION = clientCard.TITLE,
                                //                ACCOUNT_TYPE = 2,
                                //                FTFLAGS = "000000000000"
                                //            };
                                //            var jsonEmuhBase = JsonConvert.SerializeObject(muhCode1, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                                //            accountPostId = _logController.InsertPostLog(DateTime.Now, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", "/IntegrationRestApi/api/GLAccounts", string.Concat(request.Path, "/api/GLAccounts"), request.Method, jsonEmuhBase);
                                //            jResult = restService.HttpPost(TigerDataType.GLAccounts.Value, jsonEmuhBase);
                                //            _logController.InsertResponseLog(accountPostId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", "/IntegrationRestApi/api/GLAccounts", jsonEmuhBase, jResult.ToString());
                                //        }
                                //        if (first)
                                //        {
                                //            muhCode += string.Concat(s, ".");
                                //            first = false;
                                //        }
                                //        else
                                //        {
                                //            muhCode += ".";
                                //        }
                                //    }

                                //    if (DataLogic.CheckEmuAccCode(clientCard.CODE))
                                //    {
                                //        clientCard.GL_CODE = clientCard.CODE;
                                //    }
                                //}
                                //else
                                //{
                                //    accountingCode = clientCard.CODE;
                                //}
                                #endregion

                                #region Firma Kontrolü

                                if (clientCard.ISFOREIGN != 1)
                                {
                                    if (clientCard.PERSCOMPANY == 1 && !string.IsNullOrEmpty(clientCard.TCKNO))
                                    {
                                        if (!string.IsNullOrEmpty(clientCard.NAME) && !string.IsNullOrEmpty(clientCard.SURNAME))
                                        {
                                            if (clientCard.TCKNO.Length == 11)
                                            {
                                                isSend = true;
                                            }
                                            else
                                            {
                                                isSend = false;
                                                jResult = string.Concat(clientCard.TCKNO, @" Girilen TC Kimlik numarası 11 karakter olmalıdır!");
                                                _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, json, jResult.ToString());
                                            }
                                        }
                                        else
                                        {
                                            isSend = false;
                                            jResult = @"Şahıs firması için adı soyadı bilgisi zorunludur.";
                                            _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
                                                "", request.Path, json,
                                                jResult.ToString());
                                        }
                                        if (string.IsNullOrEmpty(clientCard.E_MAIL))
                                        {
                                            isSend = false;
                                            jResult = @"Şahıs firması için eposta bilgisi bilgisi zorunludur.";
                                            _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
                                                "", request.Path, json,
                                                jResult.ToString());
                                        }
                                    }
                                    else if ((clientCard.PERSCOMPANY == 0 || clientCard.PERSCOMPANY == null) && !string.IsNullOrEmpty(clientCard.TAX_ID))
                                    {
                                        if (clientCard.TAX_ID.Length == 10)
                                        {
                                            var gibInfo = ConnectService.CheckGibUser(clientCard.TAX_ID);
                                            if (!gibInfo.IsGibUser)
                                            {
                                                isSend = false;
                                                jResult = string.Concat(clientCard.TAX_ID, @" ilgili vergi kimlik numarası doğrulanamadı. Lütfen geçerli bir vergi kimlik numarası giriniz!");
                                            }
                                            else
                                            {
                                                clientCard.ACCEPT_EINV = gibInfo.IsGibUser.ToByte();
                                                clientCard.POST_LABEL = gibInfo.PkLabel;
                                                clientCard.SENDER_LABEL = gibInfo.GbLabel;
                                                isSend = true;
                                            }

                                        }
                                        else
                                        {
                                            isSend = false;
                                            jResult = string.Concat(clientCard.TAX_ID, @" Girilen Vergi Kimlik numarası 10 karakter olmalıdır!");
                                            _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
                                                "", request.Path, json,
                                                jResult.ToString());
                                        }
                                    }
                                    else
                                    {
                                        isSend = false;
                                        jResult = "Vergi Kimlik veya TC Kimlik numarası boş geçilemez!";
                                    }
                                }
                                else
                                {
                                    isSend = true;
                                }

                                #endregion
                                //else
                                //{
                                //    isSend = false;
                                //    jResult = "İlgili Cari Hesap Kartı logo'da mevcut olduğundan gönderilemedi!";
                                //}
                                //clientCard.GL_CODE = accountingCode;
                                json = JsonConvert.SerializeObject(clientCard, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                                serviceResult.JResult = restService.HttpPost(TigerDataType.Arps.Value, json);




                            }
                            else
                            {
                                //isSend = false;
                                //jResult = string.Concat(DataLogic.GetClientRefByCode(clientCard.CODE), " - ", clientCard.CODE, " ", clientCard.TITLE, " İlgili Cari Hesap Kartı logo'da mevcut olduğundan gönderilemedi!");
                                //_logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, json, jResult.ToString());
                                serviceResult.JResult = restService.HttpPut(TigerDataType.Arps.Value, clientInfo.Lref, json);
                            }
                            //if (isSend)
                            //{

                            //}

                            if (serviceResult.JResult["INTERNAL_REFERENCE"].ToObject<int>() > 0)
                            {
                                if (serviceResult.JResult != null && !serviceResult.JResult.ToString().Contains("Message"))
                                {
                                    serviceResult.Success = true;
                                    serviceResult.LogRef = serviceResult.JResult["INTERNAL_REFERENCE"].ToObject<int>();
                                    serviceResult.ObjectNo = DataLogic.GetClientCodeByRef(serviceResult.LogRef);
                                }
                                _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, json, jResult.ToString());
                            }
                            else
                            {
                                _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, json, jResult.ToString());

                                _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, jResult.ToString(), json, jResult.ToString());
                            }
                        }
                        else
                        {
                            jResult = string.Concat(@"Token alma başarısız!", tokenHolder.Token);
                            _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, jResult.ToString(), json, jResult.ToString());
                        }
                    }
                    else
                    {
                        jResult = "Cari kartı kodunu boş gönderemezsiniz";
                        _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, json, jResult.ToString());
                        _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, jResult.ToString(), json, jResult.ToString());
                    }

                }
                catch (Exception exception)
                {
                    LogHelper.LogError(string.Concat("ERROR: ", typeof(InvoiceController), MethodBase.GetCurrentMethod().Name, exception.Message));
                    _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, json, jResult.ToString());
                    _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, exception.Message, json, jResult.ToString());
                    jResult = exception.Message;
                }
            }
            else
            {
                jResult = "Göndermiş olduğunuz clientCard parametresi hatalıdır!";
                _logController.InsertResponseLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, json, jResult.ToString());
                _logController.InsertErrorLog(postId, request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(), "", request.Path, nameof(InvoiceController), MethodBase.GetCurrentMethod().Name, jResult.ToString(), json, jResult.ToString());
            }
            return serviceResult;
        }

        public ServiceResult ArpSlip(ArpSlip arpSlips, HttpContext httpContext)
        {
            var serviceResult = new ServiceResult();
            string json = string.Empty;
            RequestDto requestDto = null;
            var request = httpContext.Request;
            var tokenHolder = TokenHolder.GetInstance();
            var restService = new TigerRestService();
            try
            {
                requestDto = new RequestDto
                {
                    RequestDate = DateTime.Now,
                    RequestIp = request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
                    UserIdentity = "",
                    RequestUrl = request.Path,
                    RequestMethod = request.Method,
                    RequestData = json
                };
                arpSlips.DataObjectParameter = new DataObjectParameter();
                json = JsonConvert.SerializeObject(arpSlips, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                requestDto.RequestData = json;
                postId = _logController.InsertPostLog(requestDto);

                if (restService.GetAccessToken().IsLoggedIn)
                {
                    serviceResult.JResult = restService.HttpPost(TigerDataType.ArpSlips.Value, json);
                    if (serviceResult.JResult != null && !serviceResult.JResult.ToString().Contains("Message"))
                    {
                        serviceResult.Success = true;
                        serviceResult.LogRef = serviceResult.JResult["INTERNAL_REFERENCE"].ToObject<int>();
                        serviceResult.ObjectNo = DataLogic.GetClFicheNumberByRef(serviceResult.LogRef);
                        if (serviceResult.JResult != null && !serviceResult.JResult.ToString().Contains("Message"))
                        {

                        }
                        _logController.InsertResponseLog(postId, requestDto?.RequestIp, requestDto?.UserIdentity, requestDto?.RequestUrl, json, serviceResult.JResult?.ToString());
                    }
                }
                else
                {
                    serviceResult.JResult = string.Concat("Token alma başarısız! ", tokenHolder.Token);
                    _logController.InsertErrorLog(postId, requestDto.RequestIp, requestDto.UserIdentity,
                        requestDto.RequestUrl, nameof(ServiceManager), MethodBase.GetCurrentMethod()?.Name,
                        serviceResult.JResult.ToString(), json, serviceResult.JResult.ToString());

                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Concat("ERROR: ", nameof(ServiceManager), " ", MethodBase.GetCurrentMethod()?.Name, " ", ex.Message));
                _logController.InsertResponseLog(postId, requestDto?.RequestIp, requestDto?.UserIdentity, requestDto?.RequestUrl, json, serviceResult.JResult?.ToString());
                _logController.InsertErrorLog(postId, requestDto?.RequestIp, requestDto?.UserIdentity,
                    requestDto?.RequestUrl, nameof(ServiceManager), MethodBase.GetCurrentMethod()?.Name,
                    ex.Message, json, serviceResult.JResult?.ToString());
            }
            return serviceResult;
        }
        public ServiceResult SalesInvoice(Invoice invoice, HttpContext httpContext)
        {
            var serviceResult = new ServiceResult();
            string json = string.Empty;
            RequestDto requestDto = null;
            var request = httpContext.Request;
            var tokenHolder = TokenHolder.GetInstance();
            var restService = new TigerRestService();
            try
            {
                requestDto = new RequestDto
                {
                    RequestDate = DateTime.Now,
                    RequestIp = request.HttpContext.Connection.RemoteIpAddress.ToString() ?? request.HttpContext.Connection.LocalIpAddress.ToString(),
                    UserIdentity = "",
                    RequestUrl = request.Path,
                    RequestMethod = request.Method,
                    RequestData = json
                };
                invoice.DataObjectParameter = new DataObjectParameter();
                json = JsonConvert.SerializeObject(invoice, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                requestDto.RequestData = json;
                postId = _logController.InsertPostLog(requestDto);

                if (restService.GetAccessToken().IsLoggedIn)
                {
                    serviceResult.JResult = restService.HttpPost(TigerDataType.SalesInvoices.Value, json);
                    if (serviceResult.JResult != null && !serviceResult.JResult.ToString().Contains("Message"))
                    {
                        serviceResult.Success = true;
                        serviceResult.LogRef = serviceResult.JResult["INTERNAL_REFERENCE"].ToObject<int>();
                        serviceResult.ObjectNo = DataLogic.GetClFicheNumberByRef(serviceResult.LogRef);
                        if (serviceResult.JResult != null && !serviceResult.JResult.ToString().Contains("Message"))
                        {

                        }
                        _logController.InsertResponseLog(postId, requestDto?.RequestIp, requestDto?.UserIdentity, requestDto?.RequestUrl, json, serviceResult.JResult?.ToString());
                    }
                }
                else
                {
                    serviceResult.JResult = string.Concat("Token alma başarısız! ", tokenHolder.Token);
                    _logController.InsertErrorLog(postId, requestDto.RequestIp, requestDto.UserIdentity,
                        requestDto.RequestUrl, nameof(ServiceManager), MethodBase.GetCurrentMethod()?.Name,
                        serviceResult.JResult.ToString(), json, serviceResult.JResult.ToString());

                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Concat("ERROR: ", nameof(ServiceManager), " ", MethodBase.GetCurrentMethod()?.Name, " ", ex.Message));
                _logController.InsertResponseLog(postId, requestDto?.RequestIp, requestDto?.UserIdentity, requestDto?.RequestUrl, json, serviceResult.JResult?.ToString());
                _logController.InsertErrorLog(postId, requestDto?.RequestIp, requestDto?.UserIdentity,
                    requestDto?.RequestUrl, nameof(ServiceManager), MethodBase.GetCurrentMethod()?.Name,
                    ex.Message, json, serviceResult.JResult?.ToString());
            }
            return serviceResult;
        }
    }
}
