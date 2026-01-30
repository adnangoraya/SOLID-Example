namespace MegaOrder.Domain;

public sealed record Order(
    string Id,
    string CustomerEmail,
    string ProductSKU,
    int Quantity,
    decimal Total,
    DateTimeOffset CreatedUtc);
