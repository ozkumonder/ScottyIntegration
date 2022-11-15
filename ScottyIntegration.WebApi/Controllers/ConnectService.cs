using System;
using System.Collections.Generic;
using ConnectPostbox;
using ScottyIntegration.WebApi.Core.Helper;
using ScottyIntegration.WebApi.Models.Global;

namespace ScottyIntegration.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectService
    {
        /// <summary>
        /// Parametre verilen vergi numarasına ait firma bilgisinin edevlet durumunu kontrol eder (E-Fatura mükellefi olup olmamasını kontrol eder.).
        /// </summary>
        /// <param name="taxNumber"></param>
        /// <returns></returns>
        public static EClient CheckGibUser(string taxNumber)
        {
            var ssId = string.Empty;
            string destinationPath = string.Empty;
            var gibUser = new EClient();
            try
            {
                var connectPostbox = new PostBoxServiceClient(ConnectPostbox.PostBoxServiceClient.EndpointConfiguration.PostBoxServiceEndpoint);

                var loginConnect = new ConnectPostbox.LoginType
                {
                    appStr = "TIGER",
                    userName = "0010797435",//ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).eLogoUserName,//"0010797435",                
                    passWord = "0010797435",//ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).eLogoPassword.DecryptIt(),//"0010797435",
                    version = "2.1"
                };
                if (connectPostbox.Login(loginConnect, out ssId))
                {
                    //eFaturaWebServiceResultType resS = connectPostbox.GetValidateGIBUser(ssId, new[] { "VKN=1020414068", "DOCUMENTTYPE=1" })
                    var respose = connectPostbox.GetValidateGIBUser(ssId, new List<string>() {$"VKN={taxNumber}", "DOCUMENTTYPE = 0"});
                    if (respose.outputList.Count > 1)
                    {
                        var registerTimeLength = (respose.outputList[0].IndexOf("=", StringComparison.Ordinal) + 1);
                        var registerTime = respose.outputList[0].Substring(registerTimeLength, respose.outputList[0].Length - registerTimeLength);

                        var isGibUserLength = respose.outputList[1].IndexOf("=", StringComparison.Ordinal) + 1;
                        var isGibUser = respose.outputList[1].Substring(isGibUserLength, respose.outputList[1].Length - isGibUserLength);

                        var pkLabelLength = respose.outputList[2].IndexOf("=", StringComparison.Ordinal) + 1;
                        var pkLabel = respose.outputList[2].Substring(pkLabelLength, respose.outputList[2].Length - pkLabelLength);

                        var gbLableLength = respose.outputList[3].IndexOf("=", StringComparison.Ordinal) + 1;
                        var gbLabel = respose.outputList[3].Substring(gbLableLength, respose.outputList[3].Length - gbLableLength);

                        gibUser = new EClient
                        {
                            Date = registerTime,
                            IsGibUser = isGibUser == "1" ? true : false,
                            GbLabel = gbLabel,
                            PkLabel = pkLabel
                        };
                    }
                }
                else
                {
                    LogHelper.Log(string.Concat(LogHelper.LogType.Error.ToLogType(), "Connect Servise bağlanılamadı bilgileri kontrol ediniz!"));
                }

            }
            catch (Exception e)
            {
                LogHelper.LogError(e.Message);
            }
            return gibUser;
        }
        //public static string GetInvoicePDF(int invoiceRef)
        //{
        //    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " [", invoiceRef.ToString(), "] referanslı fatura pdf çıktısı için LdxComApi'ye bağlanılıyor..."));
        //    string path = string.Empty;
        //    //LDXCComApi.Client LDXClient = (Client)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A25B166C-A906-4636-82BA-3065269402E2")));
        //    LDXCComApi.Client LDXClient = new LDXCComApi.Client();
        //    LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " [", invoiceRef.ToString(), "] referanslı fatura pdf çıktısı için LdxComApi'ye bağlandı..."));
        //    try
        //    {
        //        var user = ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).ConnectUser;
        //        var pass = ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).ConnectPass.DecryptIt();
        //        var space = ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).ConnectWorkSpace;
        //        LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " LDXComApi Login deneniyor..."));
        //        LDXClient.Login(user, pass, space);
        //        if (LDXClient.IsLoggedIn)
        //        {
        //            LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " LDXComApi Login başarılı..."));
        //            LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " LDXComApi fatura nesnesi oluşturuluyor..."));
        //            LDXCComApi.EInvoice eInvoice = LDXClient.CreateEInvoice();
        //            LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " LDXComApi fatura nesnesi oluşturuldu..."));
        //            LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), invoiceRef, " referanslı fatura paketlenecek olarak işaretleniyor..."));
        //            var fatura = eInvoice.AddUnityInvoice(invoiceRef, EInvoiceTypes.etSales);
        //            LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), " [", invoiceRef, "] referanslı fatura paketlenecek olarak işaretlendi..."));
        //            if (fatura.GUID != null)
        //            {
        //                LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), fatura.GUID, " numaralı fatura okunuyor..."));
        //                LDXCComApi.EInvoiceElement inv = eInvoice.GetByETTN(fatura.GUID);
        //                LogHelper.Log(string.Concat(LogHelper.LogType.Info.ToLogType(), fatura.GUID, " numaralı fatura okunma başarılı..."));
        //                path = string.Concat(ConfigHelper.DeserializeDatabaseConfiguration(ConfigHelper.ReadPath).FolderPath, "\\", fatura.GUID, ".pdf");

        //                LogHelper.Log(inv.SavePDFToFile(path)
        //                    ? string.Concat(LogHelper.LogType.Info.ToLogType(), fatura.GUID, " numaralı fatura ", path,
        //                        " dosya yoluna kaydedildi...")
        //                    : string.Concat(LogHelper.LogType.Error.ToLogType(), fatura.GUID, " numaralı fatura ", path,
        //                        " dosya yoluna kaydedilemedi..."));
        //            }
        //            else
        //            {
        //                LogHelper.Log(string.Concat(LogHelper.LogType.Error.ToLogType(), " ", fatura.ErrorInfo));
        //            }
        //            LDXClient.Logout();
        //        }
        //        else
        //        {
        //            path = string.Concat("LogoConnect Bağlantısı Kurulamadı!.Hata: ", LDXClient.ErrorCode.ToString(), ",", LDXClient.ErrorInfo);
        //            LogHelper.Log(string.Concat(LogHelper.LogType.Error.ToLogType(), " LogoConnect Bağlantısı Kurulamadı!.Hata: " + LDXClient.ErrorCode.ToString() + "," + LDXClient.ErrorInfo));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        path = "LogoConnect Bağlantısı Kurulurken Hata Oluştu: " + ex.Message;
        //        LogHelper.Log(string.Concat(LogHelper.LogType.Error.ToLogType(), " LogoConnect Bağlantısı Kurulurken Hata Oluştu: " + ex.Message));
        //    }
        //    finally
        //    {
        //        LDXClient = null;
        //    }

        //    return path;
        //}

        
    }
}