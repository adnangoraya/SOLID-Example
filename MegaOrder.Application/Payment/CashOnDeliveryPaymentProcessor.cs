using MegaOrder.Domain.Payment;

namespace MegaOrder.Application.Payment;

internal class CashOnDeliveryPaymentProcessor : IPaymentProcessor
{
    public bool Process(decimal amount) => amount <= 5000;
}