using MegaOrder.Application.Payment;
using MegaOrder.Domain.Email;
using MegaOrder.Domain.Payment;
using MegaOrder.Domain.Store;
using Microsoft.Extensions.Logging;

namespace MegaOrder.Application.Order;

public sealed class MegaOrderService
{
    private readonly ILogger<MegaOrderService> _logger;
    private readonly IStore _store;
    private readonly IPaymentProcessorFactory _paymentProcessorFactory;
    private readonly IEmailSender _emailSender;
    private readonly OrderCalculator _orderCalculator;

    public MegaOrderService(
        ILogger<MegaOrderService> logger,
        IStore store,
        IPaymentProcessorFactory paymentProcessor,
        IEmailSender emailSender,
        OrderCalculator orderCalculator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _paymentProcessorFactory = paymentProcessor ?? throw new ArgumentNullException(nameof(paymentProcessor));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        _orderCalculator = orderCalculator ?? throw new ArgumentNullException(nameof(orderCalculator));
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

        decimal total = _orderCalculator.CalculateTotal(request);

        IPaymentProcessor paymentProcessor = _paymentProcessorFactory.Create(request.PaymentMethod);

        bool isPaymentProcessed = paymentProcessor.Process(total);

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