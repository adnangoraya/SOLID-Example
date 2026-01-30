namespace _6.Exercise.Processors;

public sealed class CreditCardPaymentProcessor
{
    public bool Process(decimal amount) => amount > 0;
}
