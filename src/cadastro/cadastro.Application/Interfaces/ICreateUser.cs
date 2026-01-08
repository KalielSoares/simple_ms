using cadastro.Domain.Entities;
using cadastro.Application.DTOs;

namespace cadastro.Application.Interfaces;

public interface ICreateUser
{
    Task<User> CreateUser(CriarUserDTO userDTO);
}