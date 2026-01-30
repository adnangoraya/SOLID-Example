namespace SharedModels;

public sealed record Order(
    string OrderId,
    string CustomerEmail,
    string ProductSku,
    int Quantity,
    decimal Total,
    DateTimeOffset CreatedUtc);