using MediatR;
using PaymentAPI.Domain.Common;
using PaymentAPI.Domain.Entities;
using PaymentAPI.Domain.Interfaces;

namespace PaymentAPI.Application.Transactions.GetTransactionsQuery
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, PaginatedList<Payment>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetTransactionsQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaginatedList<Payment>> Handle(GetTransactionsQuery query, CancellationToken cancellationToken)
        {
            var allPayments = (await _paymentRepository.GetPaymentsAsync()).ToList();

            return PaginatedList<Payment>.Create(allPayments, query.PageNumber, query.PageSize);
        }
    }
}
