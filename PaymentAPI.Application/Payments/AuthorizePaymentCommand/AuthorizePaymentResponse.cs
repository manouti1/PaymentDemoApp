using PaymentAPI.Domain.Enums;

namespace PaymentAPI.Application.Payments.AuthorizePaymentCommand
{
    public class AuthorizePaymentResponse
    {
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
