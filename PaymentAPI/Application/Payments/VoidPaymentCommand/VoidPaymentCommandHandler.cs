using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Domain;
using PaymentAPI.Infrastructure.Data;

namespace PaymentAPI.Application.Payments.VoidPaymentCommand
{
    public class VoidPaymentCommandHandler : IRequestHandler<VoidPaymentCommand, Payment>
    {
        private readonly PaymentDbContext _context;

        public VoidPaymentCommandHandler(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> Handle(VoidPaymentCommand command, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FindAsync(command.Id);

            if (payment == null)
                throw new ArgumentException("Payment not found.");

            payment.Status = PaymentStatus.Voided;

            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return payment;
        }
    }
}
