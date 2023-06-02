using PaymentAPI.Domain.Entities;
using PaymentAPI.Domain.Enums;
using PaymentAPI.Domain.Interfaces;

namespace PaymentAPI.Application.Payments.VoidPaymentCommand
{
    public class VoidPaymentCommandHandler : MediatR.IRequestHandler<VoidPaymentCommand, Domain.Entities.Payment>
    {
        private readonly IPaymentRepository _paymentRepository;

        public VoidPaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        public async Task<Payment> Handle(VoidPaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(command.Id);

            if (payment == null)
                throw new ArgumentException("Payment not found.");

            payment.Status = PaymentStatus.Voided;

            await _paymentRepository.UpdatePaymentAsync(payment);

            return payment;
        }
    }
}
