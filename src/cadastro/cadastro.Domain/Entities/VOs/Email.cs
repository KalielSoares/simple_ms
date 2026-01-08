using System.Text.RegularExpressions;

namespace cadastro.Domain.Entities.VOs;
public sealed record Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Invalid email.");

        value = value.Trim().ToLowerInvariant();

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format.");

        return new Email(value);
    }
}
