using MegaOrder.Domain;
using MegaOrder.Domain.Store;

namespace MegaOrder.Infrastructure.Store;

internal class FileOrderStore : IStore
{
    public bool Save(Order order)
    {
        var line = $"{order.Id}|{order.CustomerEmail}|{order.ProductSKU}|{order.Quantity}|{order.Total}|{order.CreatedUtc:O}";
        File.AppendAllLines("orders.txt", [line]);
        return true;
    }
}