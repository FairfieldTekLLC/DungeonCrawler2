namespace Aetherfall.Domain.Common;

public static class Guard
{
    public static string AgainstNullOrWhiteSpace(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{name} cannot be null or whitespace.", name);
        }

        return value.Trim();
    }

    public static int AgainstNegative(int value, string name)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(name, "Value cannot be negative.");
        }

        return value;
    }

    public static decimal AgainstNegative(decimal value, string name)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(name, "Value cannot be negative.");
        }

        return value;
    }
}
