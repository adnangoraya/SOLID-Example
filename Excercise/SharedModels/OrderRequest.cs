namespace SharedModels;

public sealed record OrderRequest(
    string Email,
    string ProductSku,
    int Quantity,
    decimal UnitPrice,
    DiscountCode DiscountCode,
    ShippingSpeed ShippingSpeed,
    PaymentMethod PaymentMethod);