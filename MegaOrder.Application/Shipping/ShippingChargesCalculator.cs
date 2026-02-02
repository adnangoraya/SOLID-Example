using MegaOrder.Application.Order;
using MegaOrder.Domain.Shipping;

namespace MegaOrder.Application.Shipping;

public class ShippingChargesCalculator
{
    private const decimal DefaultShippingCharges = 5;

    private static Dictionary<ShippingSpeed, decimal> _availableShippingMethods => new()
    {
        { ShippingSpeed.Standard, 5 },
        { ShippingSpeed.Express, 15 }
    };

    internal virtual decimal Calculate(OrderRequest request)
    {
        if (_availableShippingMethods.TryGetValue(request.ShippingSpeed, out decimal shippingCharges))
        {
            return shippingCharges;
        }

        return DefaultShippingCharges;
    }
}