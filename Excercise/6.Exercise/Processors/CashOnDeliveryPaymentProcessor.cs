namespace _6.Exercise.Processors;

public sealed class CashOnDeliveryPaymentProcessor
{
    public bool Process(decimal amount) => amount <= 5000;
}
