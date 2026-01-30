using MegaOrder.Domain;
using MegaOrder.Domain.Email;

namespace MegaOrder.Infrastructure.Email;

internal class EmailSender : IEmailSender
{
    public void SendEmail(Order order)
    {
        File.AppendAllLines("emails.txt", [$"TO={order.CustomerEmail}; SUBJECT=Order {order.Id} placed; BODY=Total: {order.Total:C}"]);
    }
}