using MegaOrder.Domain.Payment;

namespace MegaOrder.Application.Payment;

internal sealed class PayPalPaymentProcessor : IPaymentProcessor
{
    public bool Process(decimal amount) => amount > 0;
}