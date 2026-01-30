namespace MegaOrder.Application;

internal class OrderRequestValidator
{
    internal static bool IsValid(OrderRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        if (string.IsNullOrWhiteSpace(request.Email) || 
            !request.Email.Contains('@') || 
            request.Quantity <= 0)
        {
            return false;
        }

        return true;
    }
}
