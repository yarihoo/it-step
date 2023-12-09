namespace Internet_Market_WebApi.Models
{
    public class PaymentFormModel
    {
        public string CardNumber { get;set; }
        public DateTime ExpireDate { get; set; }
        public int CVV { get; set; }
    }
}
