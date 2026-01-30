using MegaOrder.Application.Discount;
using MegaOrder.Application.Shipping;
using MegaOrder.Domain;
using MegaOrder.Domain.Email;
using MegaOrder.Domain.Payment;
using MegaOrder.Domain.Store;
using Microsoft.Extensions.Logging;

namespace MegaOrder.Application;

public sealed class MegaOrderService(
    ILogger<MegaOrderService> _logger, 
    IStore _store, 
    IPaymentProcessor paymentProcessor, 
    IEmailSender emailSender)
{
    public string PlaceOrder(OrderRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        _logger.LogInformation("Placing order");

        if (!OrderRequestValidator.IsValid(request))
        {
            _logger.LogWarning("Invalid order request.");
            throw new ArgumentException("Invalid order request.");
        }

        decimal discount = DiscountCalculator.Calculate(request);

        decimal shippingCharges = ShippingChargesCalculator.Calculate(request);

        decimal subtotal = request.UnitPrice * request.Quantity;

        decimal total = (subtotal - discount) + shippingCharges;

        bool isPaymentProcessed = paymentProcessor.Process(total);

        if (!isPaymentProcessed) 
        {
            _logger.LogWarning("Payment failed.");
            throw new InvalidOperationException("Payment failed.");
        }

        string orderId = Guid.NewGuid().ToString("N");

        Order order = new(orderId, request.Email, request.ProductSku, request.Quantity, total, TimeProvider.System.GetUtcNow());

        _store.Save(order);

        emailSender.SendEmail(order);

        _logger.LogInformation("Order {orderId} completed", orderId);
        return orderId;
    }
}