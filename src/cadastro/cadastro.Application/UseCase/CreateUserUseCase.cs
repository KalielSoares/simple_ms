using System.Text.Json;
using cadastro.Application.Interfaces;
using cadastro.Application.DTOs;
using cadastro.Domain.Entities;
using cadastro.Domain.Entities.VOs;
using cadastro.Application.Services;

namespace cadastro.Application.UseCase;

public class CreateUserUseCase : ICreateUser
{

    private readonly IMensageria _mensageria;
    private readonly UserEventFactory _userEvent;

    public CreateUserUseCase(IMensageria mensageria, UserEventFactory userEvent)
    {
        _mensageria = mensageria;
        _userEvent = userEvent;
    }

    public async Task<User> CreateUser(CriarUserDTO userDTO)
    {
        
        var user = new User(Name.Create(userDTO.name),Email.Create(userDTO.email));

        var userEvent = _userEvent.createdUserEvent(user);

        var payload = JsonSerializer.Serialize(userEvent);

        await _mensageria.Send(payload);

        return user;

    }
}