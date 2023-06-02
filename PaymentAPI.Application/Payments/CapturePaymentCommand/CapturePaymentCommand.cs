using MediatR;
using PaymentAPI.Domain.Entities;

namespace PaymentAPI.Application.Payments.CapturePaymentCommand
{
    public class CapturePaymentCommand : IRequest<Payment>
    {
        public Guid Id { get; set; }
        public string OrderReference { get; set; }
    }
}
