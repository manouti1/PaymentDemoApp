using MediatR;
using PaymentAPI.Domain;
using PaymentAPI.Infrastructure.Data;

namespace PaymentAPI.Application.Payments.AuthorizePaymentCommand
{
    public class AuthorizePaymentCommandHandler : IRequestHandler<AuthorizePaymentCommand, AuthorizePaymentResponse>
    {
        private readonly PaymentDbContext _context;

        public AuthorizePaymentCommandHandler(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorizePaymentResponse> Handle(AuthorizePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment(request.Amount, request.Currency, request.CardholderNumber, request.HolderName, request.ExpirationMonth, request.ExpirationYear, request.CVV, request.OrderReference);

            try
            {
                // Validate the payment details
                payment.Validate();
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid payment details", ex);
            }

            decimal balance = GetCardholderBalance(payment.CardholderNumber); // This is a placeholder method
           
            if (balance < payment.Amount)
            {
                throw new Exception("Insufficient balance");
            }

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new AuthorizePaymentResponse
            {
                Id = payment.Id,
                Status = payment.Status
            };

            return response;
        }
        private decimal GetCardholderBalance(string cardholderNumber)
        {
            return 1000; // Return a fixed balance
        }
    }
}
