using cadastro.Application.DTOs;
using cadastro.Domain.Entities;
using cadastro.Domain.Entities.VOs;

namespace cadastro.Application.Services;

public class UserEventFactory
{
    public UserCreatedMessage createdUserEvent(User userA)
    {
     
        var userEvent = new UserCreatedMessage
        (
            userA.Name.Value,
            userA.Email.Value
        );
        
        return userEvent;
    }
}