namespace PaymentAPI.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPaymentRepository Payments { get; }
    }
}
