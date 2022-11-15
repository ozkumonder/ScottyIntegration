namespace ScottyIntegration.WebApi.Models.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientInfoDto
    {
        public int Lref { get; set; }
        public string Code { get; set; }
        public string TaxNr { get; set; }
        public string Tckn { get; set; }
        public bool CheckingResult { get; set; }
    }
}
