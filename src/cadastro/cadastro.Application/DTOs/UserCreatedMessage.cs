namespace cadastro.Application.DTOs;

public class UserCreatedMessage
{
    public string EventId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    

    public UserCreatedMessage(string name, string email)
    {
        EventId = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0,9);
        Name = name;
        Email = email;
    }
}