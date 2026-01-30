using MegaOrder.Domain.Discount;
using MegaOrder.Domain.Payment;
using MegaOrder.Domain.Shipping;

namespace MegaOrder.Application;

public sealed record OrderRequest(
    string Email,
    string ProductSku,
    int Quantity,
    decimal UnitPrice,
    DiscountCode DiscountCode,
    ShippingSpeed ShippingSpeed,
    PaymentMethod PaymentMethod);
