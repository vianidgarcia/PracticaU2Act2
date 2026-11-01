namespace PracticaU2Act2;

/// <summary>User-facing error messages (in Spanish), kept in one place so every class uses the same text.</summary>
public static class ErrorMessages
{
    public const string NullArray = "El arreglo no puede ser nulo.";
    public const string EmptyInput = "Escribe un valor.";
    public const string EmptyList = "Escribe al menos un valor separado por comas.";
    public const string EmptyItem = "Hay un valor vacío entre las comas.";

    public static string FactorialRange(int max) =>
        $"n debe estar entre 0 y {max} (con un valor mayor el resultado no cabe en un long).";

    public static string FibonacciIterativeRange(int max) =>
        $"n debe estar entre 0 y {max} (con un valor mayor el resultado no cabe en un long).";

    public static string FibonacciRecursiveRange(int max) =>
        $"n debe estar entre 0 y {max} en la versión recursiva: hace una cantidad exponencial de llamadas.";

    public static string HanoiRange(int min, int max) =>
        $"El número de discos debe estar entre {min} y {max}.";

    public static string NotAnInteger(string text) =>
        $"\"{text}\" no es un número entero válido.";

    public static string TooManyValues(int max) =>
        $"Solo se admiten hasta {max} valores (la recursión usa una llamada por cada elemento).";
}
