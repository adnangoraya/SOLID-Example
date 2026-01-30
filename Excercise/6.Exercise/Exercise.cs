using _6.Exercise.Infrastructure;
using _6.Exercise.Processors;
using _6.Exercise.Stores;
using SharedModels;
using Microsoft.Extensions.DependencyInjection;

namespace _6.Exercise;

// This project is intentionally "bad" and violates all SOLID principles.
// This needs refactoring to follow SOLID principles.
//
// Violations included:
// - SRP
// - OCP
// - LSP
// - ISP
// - DIP

public sealed class MegaOrderService
{
    private readonly OrderStore _store;

    public MegaOrderService(IServiceProvider serviceProvider)
    {
        _store = serviceProvider.GetRequiredService<OrderStore>()!;
    }

    public string PlaceOrder(OrderRequest request)
    {
        if (!request.Email.Contains('@'))
        {
            throw new ArgumentException("Email is not valid", nameof(request));
        }

        if (request.Quantity <= 0)
        {
            throw new ArgumentException("Quantity must be > 0", nameof(request));
        }

        FileLogger logger = new("exercise-log.txt");
        logger.Log($"Placing order for {request.Email}");

        decimal subtotal = request.UnitPrice * request.Quantity;

        decimal discount = request.DiscountCode switch
        {
            DiscountCode.None => 0,
            DiscountCode.Employee => subtotal * 0.20m,
            DiscountCode.Seasonal => subtotal * 0.10m,
            _ => 0
        };

        decimal shipping = request.ShippingSpeed switch
        {
            ShippingSpeed.Standard => 5,
            ShippingSpeed.Express => 15,
            _ => 5
        };

        decimal total = subtotal - discount + shipping;

        bool paid = request.PaymentMethod switch
        {
            PaymentMethod.CreditCard => new CreditCardPaymentProcessor().Process(total),
            PaymentMethod.PayPal => new PayPalPaymentProcessor().Process(total),
            PaymentMethod.CashOnDelivery => new CashOnDeliveryPaymentProcessor().Process(total),
            _ => false
        };

        if (!paid)
        {
            throw new InvalidOperationException("Payment failed");
        }

        Order order = new(
            OrderId: Guid.NewGuid().ToString("N"),
            CustomerEmail: request.Email,
            ProductSku: request.ProductSku,
            Quantity: request.Quantity,
            Total: total,
            CreatedUtc: DateTimeOffset.UtcNow);

        _store.Save(order);

        SmtpEmailer emailer = new();
        emailer.Send(order.CustomerEmail, $"Order {order.OrderId} placed", $"Total: {order.Total:C}");

        logger.Log($"Order {order.OrderId} completed");
        return order.OrderId;
    }
}