using MediatR;
using PaymentAPI.Domain.Entities;
using PaymentAPI.Domain.Interfaces;

namespace PaymentAPI.Application.Payments.AuthorizePaymentCommand
{
    public class AuthorizePaymentCommandHandler : IRequestHandler<AuthorizePaymentCommand, AuthorizePaymentResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        public AuthorizePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
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

            var paymentId = await _paymentRepository.AuthorizePaymentAsync(payment);

            var response = new AuthorizePaymentResponse
            {
                Id = paymentId,
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
