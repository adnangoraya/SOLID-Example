namespace MegaOrder.Domain.Store;

public interface IStore
{
    bool Save(Order order);
}