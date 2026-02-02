using MegaOrder.Application.Discount;
using MegaOrder.Application.Shipping;

namespace MegaOrder.Application.Order;

public class OrderCalculator
{
    private readonly DiscountCalculator _discountCalculator;
    private readonly ShippingChargesCalculator _shippingChargesCalculator;

    public OrderCalculator(DiscountCalculator discountCalculator, ShippingChargesCalculator shippingChargesCalculator)
    {
        _discountCalculator = discountCalculator ?? throw new ArgumentNullException(nameof(discountCalculator));
        _shippingChargesCalculator = shippingChargesCalculator ?? throw new ArgumentNullException(nameof(shippingChargesCalculator));
    }

    internal virtual decimal CalculateTotal(OrderRequest request)
    {
        decimal discount = _discountCalculator.Calculate(request);

        decimal shippingCharges = _shippingChargesCalculator.Calculate(request);

        decimal subtotal = request.UnitPrice * request.Quantity;

        decimal total = (subtotal - discount) + shippingCharges;

        return total;
    }
}