using SharedModels;

namespace _6.Exercise.Stores;

public sealed class ReadOnlyOrderStore : OrderStore
{
    public override void Save(Order order)
    {
        throw new NotSupportedException("This store is read-only.");
    }
}
