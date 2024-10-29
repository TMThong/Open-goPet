namespace GopetHost.Models
{
    public class BankQRModel
    {
        public BankModel BankModel { get; set; }

        public string QRCode { get; set; } = string.Empty;

        public string NapContent { get; set; } = string.Empty;
    }
}
