namespace cadastro.Application.DTOs;

/// <summary>
/// MENSAGEM / EVENTO: Representa o contrato de um usuário criado.
/// ORIGEM: Microserviço de Cadastro (via CreateUserUseCase).
/// DESTINO: Fila do RabbitMQ lida pelo Worker de Persistência.
/// </summary>
/// <remarks>
/// Esta classe deve ser idêntica (ou compatível) no Producer e no Consumer.
/// O EventId é gerado automaticamente para fins de rastreabilidade (Tracing).
/// </remarks>
public class UserCreatedMessage
{
    /// <summary> Identificador único do evento (ex: para logs e auditoria) </summary>
    public string EventId { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }

    /// <summary>
    /// Construtor que inicializa o evento com os dados básicos e gera um ID curto único.
    /// </summary>
    /// <param name="name">Nome completo do usuário vindo do DTO de entrada</param>
    /// <param name="email">E-mail validado do usuário</param>
    public UserCreatedMessage(string name, string email)
    {
        // Gera um ID amigável de 9 caracteres para facilitar a busca em logs
        EventId = Guid.NewGuid().ToString().Replace("-", "").ToUpper().Substring(0, 9);
        Name = name;
        Email = email;
    }
}