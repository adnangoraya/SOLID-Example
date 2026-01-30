namespace _6.Exercise.Infrastructure;

public sealed class SmtpEmailer
{
    public void Send(string to, string subject, string body)
    {
        File.AppendAllLines("emails.txt", [$"TO={to}; SUBJECT={subject}; BODY={body}"]);
    }
}
