namespace PaymentAPI.Models
{
    public class PaymentRequest
    {
        public int Amount { get; set; }
        public string CardNumber { get; set; }
    }
}