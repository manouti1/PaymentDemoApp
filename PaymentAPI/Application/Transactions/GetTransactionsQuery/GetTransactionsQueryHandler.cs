using MediatR;
using PaymentAPI.Domain;
using PaymentAPI.Infrastructure.Data;

namespace PaymentAPI.Application.Transactions.GetTransactionsQuery
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, PaginatedList<Payment>>
    {
        private readonly PaymentDbContext _context;

        public GetTransactionsQueryHandler(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Payment>> Handle(GetTransactionsQuery query, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .OrderByDescending(p => p.Id)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
