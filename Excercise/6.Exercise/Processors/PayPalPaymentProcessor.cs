namespace _6.Exercise.Processors;

public sealed class PayPalPaymentProcessor
{
    public bool Process(decimal amount) => amount > 0;
}
