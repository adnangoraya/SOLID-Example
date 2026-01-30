using SharedModels;

namespace _6.Exercise.Stores;

public abstract class OrderStore
{
    public virtual void Save(Order order)
    {
        var line = $"{order.OrderId}|{order.CustomerEmail}|{order.ProductSku}|{order.Quantity}|{order.Total}|{order.CreatedUtc:O}";
        File.AppendAllLines("orders.txt", [line]);
    }
}
