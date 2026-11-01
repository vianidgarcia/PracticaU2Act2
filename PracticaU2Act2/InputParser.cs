using System.Globalization;

namespace PracticaU2Act2;

/// <summary>Turns the text typed by the user into numbers, with clear errors in Spanish.</summary>
public static class InputParser
{
    public static int ParseInt(string text)
    {
        string trimmed = (text ?? string.Empty).Trim();
        if (trimmed.Length == 0)
        {
            throw new FormatException(ErrorMessages.EmptyInput);
        }

        if (!int.TryParse(trimmed, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
        {
            throw new FormatException(ErrorMessages.NotAnInteger(trimmed));
        }

        return value;
    }

    /// <summary>Parses "4, 8, 15" into an array. Rejects empty text, empty items and lists that are too long.</summary>
    public static int[] ParseIntList(string text)
    {
        string trimmed = (text ?? string.Empty).Trim();
        if (trimmed.Length == 0)
        {
            throw new FormatException(ErrorMessages.EmptyList);
        }

        string[] parts = trimmed.Split(',');
        if (parts.Length > RecursiveCatalog.MaxArrayLength)
        {
            throw new FormatException(ErrorMessages.TooManyValues(RecursiveCatalog.MaxArrayLength));
        }

        var values = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            string part = parts[i].Trim();
            if (part.Length == 0)
            {
                throw new FormatException(ErrorMessages.EmptyItem);
            }

            values[i] = ParseInt(part);
        }

        return values;
    }
}
