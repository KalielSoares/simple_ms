namespace notification.Application.DTOs;

public record UserDTO(string UserId, string EventId, string Name, string Email, string CreatedAt);