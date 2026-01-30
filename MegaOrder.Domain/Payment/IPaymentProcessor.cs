namespace MegaOrder.Domain.Payment;

public interface IPaymentProcessor
{
    bool Process(decimal amount);
}