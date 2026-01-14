namespace notification.Domain.Entities;

public class User
{
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;

    public User(string userId,string name,string email,string createdAt)
    {
        UserId = userId;
        Name = name;
        Email = email;
        CreatedAt = createdAt;
    }
    
}