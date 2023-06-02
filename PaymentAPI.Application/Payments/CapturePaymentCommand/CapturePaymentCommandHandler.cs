using PaymentAPI.Domain.Interfaces;

namespace PaymentAPI.Application.Payments.CapturePaymentCommand
{
    public class CapturePaymentCommandHandler : MediatR.IRequestHandler<CapturePaymentCommand, Domain.Entities.Payment>
    {
        private readonly IPaymentRepository _paymentRepository;

        public CapturePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        public async Task<Domain.Entities.Payment> Handle(CapturePaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(command.Id);

            if (payment == null)
                throw new ArgumentException("Payment not found.");

            if (payment.Status == Domain.Enums.PaymentStatus.Authorized)
            {
                payment.Status = Domain.Enums.PaymentStatus.Captured;
                await _paymentRepository.UpdatePaymentAsync(payment);
            }
            else
            {
                throw new InvalidOperationException("Only authorized payments can be captured.");
            }

            return payment;
        }
    }
}
