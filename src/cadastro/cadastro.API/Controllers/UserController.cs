using cadastro.Application.DTOs;
using cadastro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cadastro.API.Controllers;

/// <summary>
/// FLUXO: Program -> Controller
/// </summary>
/// <remarks>
/// Foi feito como um primary constructor no trecho onde o ICreateUser usa o "AddScoped" que faz o DE-PARA
/// no program e usa os metodos do CreateUserUseCase
/// </remarks>
/// <param name="createUserUseCase"></param>

[ApiController]
[Route("/[Controller]")]
public class UserController(ICreateUser createUserUseCase) : ControllerBase
{
    /// <summary>
    /// ENTRADA: Endpoint público para criação de novas ordens.
    /// FLUXO: Controller -> Application Service -> RabbitMQ (Event)
    /// </summary>
    /// <remarks>
    /// Este método apenas valida o DTO e repassa para a camada de aplicação.
    /// A persistência real NÃO acontece aqui; ela é feita pelo Worker de forma assíncrona.
    /// </remarks>
    /// <param name="userDto">Dados da ordem vindos do Front-end/Consumidor</param>
    /// <returns>Retorna 202 (Accepted) indicando que o processamento começou.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateUserRequest([FromBody]CriarUserDTO userDto)
    {
        await createUserUseCase.CreateUser(userDto);
        return Accepted();
    }
}