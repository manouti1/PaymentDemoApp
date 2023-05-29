using MediatR;

namespace PaymentAPI.Application.Payments.AuthorizePaymentCommand
{
    public class AuthorizePaymentCommand : IRequest<AuthorizePaymentResponse>
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardholderNumber { get; set; }
        public string HolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVV { get; set; }
        public string OrderReference { get; set; }
    }
}
