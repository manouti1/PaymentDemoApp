using MediatR;
using PaymentAPI.Domain;
using PaymentAPI.Infrastructure.Data;

namespace PaymentAPI.Application.Transactions.GetTransactionsQuery
{
    public class GetTransactionsQuery : IRequest<PaginatedList<Payment>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
