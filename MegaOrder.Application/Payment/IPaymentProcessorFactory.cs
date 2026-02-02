using MegaOrder.Domain.Payment;

namespace MegaOrder.Application.Payment;

public interface IPaymentProcessorFactory
{
    public IPaymentProcessor Create(PaymentMethod paymentMethod);
}
