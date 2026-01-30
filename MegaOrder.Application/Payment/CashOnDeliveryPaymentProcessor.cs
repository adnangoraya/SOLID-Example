using MegaOrder.Domain.Payment;

namespace MegaOrder.Application.Payment;

internal sealed class CashOnDeliveryPaymentProcessor : IPaymentProcessor
{
    private const decimal MinimumCashOnDeliveryOrderAmount = 5000;

    public bool Process(decimal amount) => amount <= MinimumCashOnDeliveryOrderAmount;
}