using MediatR;
using PaymentAPI.Domain.Entities;

namespace PaymentAPI.Application.Transactions.GetTransactionsQuery
{
    public class GetTransactionsQuery : IRequest<Domain.Common.PaginatedList<Payment>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
