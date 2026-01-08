using cadastro.Domain.Entities.VOs;

namespace cadastro.Domain.Entities;

public class User
{
    public string userId { get; set; }
    public Name Name { get; set; }

    public Email Email { get; set; }

    public User(Name name, Email email)
    {
        userId = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0,9);
        Name = name;
        Email = email;
    }
}