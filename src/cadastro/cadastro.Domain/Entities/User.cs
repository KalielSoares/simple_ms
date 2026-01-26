using cadastro.Domain.Entities.VOs;

namespace cadastro.Domain.Entities;

public class User
{
    public Name Name { get; set; }

    public Email Email { get; set; }

    public User(Name name, Email email)
    {
        Name = name;
        Email = email;
    }
}