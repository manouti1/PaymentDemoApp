using MediatR;
using PaymentAPI.Domain.Entities;

namespace PaymentAPI.Application.Payments.VoidPaymentCommand
{
    public class VoidPaymentCommand : IRequest<Payment>
    {
        public Guid Id { get; set; }
        public string OrderReference { get; set; }
    }
}
