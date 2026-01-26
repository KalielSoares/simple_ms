using notification.Application.DTOs;
using notification.Application.Interfaces;
using notification.Application.UseCases;
using notification.Domain.Entities;

namespace notification.Infrastructure.Persistence.Repository;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task createUserRepository(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
}