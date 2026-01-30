using MegaOrder.Application.Order;
using MegaOrder.Domain.Shipping;

namespace MegaOrder.Application.Shipping;

internal sealed class ShippingChargesCalculator
{
    private const decimal DefaultShippingCharges = 5;

    private static Dictionary<ShippingSpeed, decimal> _availableShippingMethods => new()
    {
        { ShippingSpeed.Standard, 5 },
        { ShippingSpeed.Express, 15 }
    };

    internal static decimal Calculate(OrderRequest request)
    {
        if (_availableShippingMethods.TryGetValue(request.ShippingSpeed, out decimal shippingCharges))
        {
            return shippingCharges;
        }

        return DefaultShippingCharges;
    }
}