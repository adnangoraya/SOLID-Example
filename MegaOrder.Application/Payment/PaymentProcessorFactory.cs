using MegaOrder.Domain.Payment;

namespace MegaOrder.Application.Payment;

internal class PaymentProcessorFactory : IPaymentProcessorFactory
{
    Func<PaymentMethod, IPaymentProcessor> _factory;

    public PaymentProcessorFactory(Func<PaymentMethod, IPaymentProcessor> factory)
    {
        _factory = factory;
    }

    public IPaymentProcessor Create(PaymentMethod paymentMethod)
    {
        IPaymentProcessor? paymentProcessor = _factory(paymentMethod);

        return paymentProcessor ?? throw new Exception("Payment method not supported.");
    }
}
