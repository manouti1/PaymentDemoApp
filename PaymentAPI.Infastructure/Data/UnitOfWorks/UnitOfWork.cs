using PaymentAPI.Domain.Interfaces;
using PaymentAPI.Infastructure.Data.Repositories;

namespace PaymentAPI.Infastructure.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private IPaymentRepository _payments;

        public UnitOfWork(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IPaymentRepository Payments
        {
            get { return _payments ??= new PaymentRepository(_connectionFactory); }
        }
    }
}
