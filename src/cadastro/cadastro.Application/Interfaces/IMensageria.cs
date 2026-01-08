namespace cadastro.Application.Interfaces;

public interface IMensageria
{
    Task Send(string message);
}