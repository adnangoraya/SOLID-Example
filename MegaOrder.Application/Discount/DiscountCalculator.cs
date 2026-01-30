using MegaOrder.Domain.Discount;

namespace MegaOrder.Application.Discount;

public class DiscountCalculator
{
    private const decimal NoDiscount = 0;

    private static Dictionary<DiscountCode, decimal> _availableDiscounts => new()
    {
        { DiscountCode.None, 0 },
        { DiscountCode.Employee, 0.20m },
        { DiscountCode.Seasonal, 0.10m }
    };

    internal static decimal Calculate(OrderRequest request)
    {
        if (_availableDiscounts.TryGetValue(request.DiscountCode, out decimal discountValue))
        {
            decimal subTotal = request.UnitPrice * request.Quantity;
            decimal discount = subTotal * discountValue;

            return discount;
        }

        return NoDiscount;
    }
}