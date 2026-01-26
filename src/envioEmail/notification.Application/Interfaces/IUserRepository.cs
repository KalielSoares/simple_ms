using notification.Application.DTOs;
using notification.Domain.Entities;

namespace notification.Application.Interfaces;

public interface IUserRepository
{
    Task createUserRepository(User user);
}