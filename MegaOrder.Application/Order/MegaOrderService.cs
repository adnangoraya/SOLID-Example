using MegaOrder.Application.Discount;
using MegaOrder.Application.Shipping;
using MegaOrder.Domain.Email;
using MegaOrder.Domain.Payment;
using MegaOrder.Domain.Store;
using Microsoft.Extensions.Logging;

namespace MegaOrder.Application.Order;

public sealed class MegaOrderService
{
    private readonly ILogger<MegaOrderService> _logger;
    private readonly IStore _store;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IEmailSender _emailSender;
    
    public MegaOrderService(
        ILogger<MegaOrderService> logger,
        IStore store,
        IPaymentProcessor paymentProcessor,
        IEmailSender emailSender)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _paymentProcessor = paymentProcessor ?? throw new ArgumentNullException(nameof(paymentProcessor));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
    }

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

        bool isPaymentProcessed = _paymentProcessor.Process(total);

        if (!isPaymentProcessed) 
        {
            _logger.LogWarning("Payment failed.");
            throw new InvalidOperationException("Payment failed.");
        }

        string orderId = Guid.NewGuid().ToString("N");

        Domain.Order order = new(orderId, request.Email, request.ProductSku, request.Quantity, total, TimeProvider.System.GetUtcNow());

        _store.Save(order);

        _emailSender.SendEmail(order);

        _logger.LogInformation("Order {orderId} completed", orderId);
        return orderId;
    }
}