using cadastro.Application.DTOs;
using cadastro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cadastro.API.Controllers;

[ApiController]
[Route("/[Controller]")]
public class UserController : ControllerBase
{
    private readonly ICreateUser _createUserUseCase;
    public UserController(ICreateUser createUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody]CriarUserDTO userDTO)
    {
        var userId = await _createUserUseCase.CreateUser(userDTO);
        return CreatedAtAction(nameof(Criar), userDTO);
    }
}