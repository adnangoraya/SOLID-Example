namespace MegaOrder.Domain.Email;

public interface IEmailSender
{
    void SendEmail(Order order);
}