using MegaOrder.Domain;
using MegaOrder.Domain.Store;

namespace MegaOrder.Infrastructure.Store;

public class OrderStore : IStore
{
    HashSet<Order> _orders = [];

    public bool Save(Order order)
    {
        _orders.Add(order);
        return true;
    }
}