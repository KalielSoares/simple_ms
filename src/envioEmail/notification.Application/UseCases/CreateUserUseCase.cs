using notification.Application.DTOs;
using notification.Application.Interfaces;
using notification.Domain.Entities;

namespace notification.Application.UseCases;

public class CreateUserUseCase(IUserRepository repository)
{
    public async Task Execute(UserDTO dto)
    {
        var user = new User(
            Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper().Substring(0, 10),
            dto.Name,
            dto.Email,
            DateTime.UtcNow.AddHours(-3).ToString("dd-MM-yyyy HH:mm:ss")
        );

        await repository.createUserRepository(user);
    }
}