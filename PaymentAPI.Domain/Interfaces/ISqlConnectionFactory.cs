using System.Data;

namespace PaymentAPI.Domain.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
        Task Init();
    }
}
