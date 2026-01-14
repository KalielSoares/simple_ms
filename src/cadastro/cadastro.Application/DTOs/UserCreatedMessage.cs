namespace cadastro.Application.DTOs;

public class UserCreatedMessage
{
    public string EventId { get; set; }
    public string userId { get; set;}
    public string Name { get; set; }
    public string Email { get; set; }

    public string createdAt { get; set; }

    public UserCreatedMessage(string userid,string name, string email)
    {
        EventId = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0,9);
        userId = userid;
        Name = name;
        Email = email;
        createdAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");;   
    }
}