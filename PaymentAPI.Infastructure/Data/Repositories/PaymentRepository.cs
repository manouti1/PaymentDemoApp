using Dapper;
using Mapster;
using Microsoft.Data.Sqlite;
using PaymentAPI.Domain.Entities;
using PaymentAPI.Domain.Interfaces;
using PaymentAPI.Infastructure.Data.Models;
using System;

namespace PaymentAPI.Infastructure.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ISqlConnectionFactory _dbConnection;

        public PaymentRepository(ISqlConnectionFactory dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        public async Task<Guid> AuthorizePaymentAsync(Payment payment)
        {
            using var connection = _dbConnection.CreateConnection();

            var sql = "INSERT INTO Payment (Id, Amount, Currency, HolderName, CardholderNumber, ExpirationMonth, ExpirationYear, CVV, OrderReference, Status) VALUES (@Id, @Amount, @Currency, @HolderName, @CardholderNumber, @ExpirationMonth, @ExpirationYear, @CVV, @OrderReference, @Status);";
            await connection.ExecuteAsync(sql, new { payment.Id, payment.Amount, payment.Currency, payment.HolderName, payment.CardholderNumber, payment.ExpirationMonth, payment.ExpirationYear, payment.CVV, payment.OrderReference, payment.Status });
            return payment.Id;
        }
        public async Task<Payment> GetPaymentByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Payment WHERE Id = @Id";

            using (var connection = _dbConnection.CreateConnection())
            {
                var paymentDTO = await connection.QueryFirstOrDefaultAsync<PaymentDTO>(sql, new { Id = id.ToString().ToUpper() });
                var payment = paymentDTO.Adapt<Payment>();

                return payment;
            }
        }

        public async Task<bool> UpdatePaymentAsync(Payment payment)
        {
            var sql = "UPDATE Payment SET Status = @Status WHERE Id = @Id";

            using (var connection = _dbConnection.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(sql, new
                {
                    Id = payment.Id.ToString().ToUpper(),
                    Status = payment.Status,
                });

                return rowsAffected > 0;

            }
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            using var connection = _dbConnection.CreateConnection();

            var sql = "SELECT * FROM Payment";
            var paymentDTOList = await connection.QueryAsync<PaymentDTO>(sql);
            var paymentList = paymentDTOList.Adapt<List<Payment>>();

            return paymentList;
        }
    }
}
