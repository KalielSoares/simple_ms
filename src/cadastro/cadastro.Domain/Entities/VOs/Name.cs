namespace cadastro.Domain.Entities.VOs;

public sealed record Name
{
    public string Value { get; private set; }

    public Name(string value)
    {
        Value = value;
    }

    public static Name Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be empty.");

        value = value.Trim();

        if (value.Length < 2)
            throw new ArgumentException("Name must have at least 2 characters.");

        if (value.Length > 100)
            throw new ArgumentException("Name cannot exceed 100 characters.");

        return new Name(value);
    }
}