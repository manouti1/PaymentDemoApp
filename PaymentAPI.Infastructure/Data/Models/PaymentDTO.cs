using PaymentAPI.Domain.Enums;

namespace PaymentAPI.Infastructure.Data.Models
{
    public class PaymentDTO
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardholderNumber { get; set; }
        public string HolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVV { get; set; }
        public string OrderReference { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
