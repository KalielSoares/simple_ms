namespace notification.Application.Interfaces;

public interface IMensageria
{
    Task Recieve(string message);
}