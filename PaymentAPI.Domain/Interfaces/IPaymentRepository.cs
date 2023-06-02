using PaymentAPI.Domain.Entities;

namespace PaymentAPI.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Guid> AuthorizePaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(Guid id);
        Task<bool> UpdatePaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsAsync();

    }
}
