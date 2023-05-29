using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Domain;
using PaymentAPI.Infrastructure.Data;

namespace PaymentAPI.Application.Payments.CapturePaymentCommand
{
    public class CapturePaymentCommandHandler : IRequestHandler<CapturePaymentCommand, Payment>
    {
        private readonly PaymentDbContext _context;

        public CapturePaymentCommandHandler(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> Handle(CapturePaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FindAsync(command.Id);

            if (payment == null)
                throw new ArgumentException("Payment not found.");

            if (payment.Status == PaymentStatus.Authorized)
            {
                payment.Status = PaymentStatus.Captured;
                _context.Entry(payment).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("Only authorized payments can be captured.");
            }

            return payment;
        }
    }
}
