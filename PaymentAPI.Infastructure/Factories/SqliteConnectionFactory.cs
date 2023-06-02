using Dapper;
using Microsoft.Data.Sqlite;
using PaymentAPI.Domain.Interfaces;
using System.Data;

namespace PaymentAPI.Infastructure.Factories
{
    public class SqliteConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqliteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateConnection()
        {
            // SQLite automatically creates the file if it doesn't exist when a connection is opened
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public async Task Init()
        {
            using var connection = CreateConnection();

            try
            {
                await _initPayments();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while initializing the database: {ex}");
                throw;
            }

            async Task _initPayments()
            {
                var sql = @"
                CREATE TABLE IF NOT EXISTS Payment(
                    Id Text PRIMARY KEY,
                    Amount DECIMAL NOT NULL,
                    Currency TEXT NOT NULL,
                    CardholderNumber TEXT NOT NULL,
                    HolderName TEXT NOT NULL,
                    ExpirationMonth INT NOT NULL,
                    ExpirationYear INT NOT NULL,
                    CVV INT NOT NULL,
                    OrderReference TEXT NOT NULL,
                    Status INT NOT NULL
                );";
                await connection.ExecuteAsync(sql);
                Console.WriteLine("Init method finished."); // Add this line.
            }
        }
    }
}
