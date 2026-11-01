using System.Text;

namespace PracticaU2Act2;

/// <summary>Order matches the list in <see cref="ExerciseCatalogInfo"/> and the combo box.</summary>
public enum ExerciseKind
{
    Factorial = 0,
    Fibonacci = 1,
    Hanoi = 2,
    ArraySum = 3,
    LinearSearch = 4
}

/// <summary>Text that documents one exercise: its base case, its recursive case and its complexity.</summary>
public sealed record ExerciseInfo(
    string Name,
    string InputLabel,
    string SampleInput,
    bool NeedsTarget,
    string SampleTarget,
    string Statement,
    string BaseCase,
    string RecursiveCase,
    string IterativeIdea,
    string TimeComplexity,
    string SpaceComplexity)
{
    public string ToDescription()
    {
        var sb = new StringBuilder();
        sb.AppendLine(Name).AppendLine();
        sb.AppendLine("Problema:").AppendLine(Statement).AppendLine();
        sb.AppendLine("Caso base (condición de salida):").AppendLine(BaseCase).AppendLine();
        sb.AppendLine("Caso recursivo (llamada recursiva):").AppendLine(RecursiveCase).AppendLine();
        sb.AppendLine("Versión iterativa:").AppendLine(IterativeIdea).AppendLine();
        sb.AppendLine("Complejidad:");
        sb.AppendLine($"Tiempo: {TimeComplexity}");
        sb.Append($"Espacio: {SpaceComplexity}");
        return sb.ToString();
    }
}

public static class ExerciseCatalogInfo
{
    public static readonly IReadOnlyList<ExerciseInfo> All = new[]
    {
        new ExerciseInfo(
            "Factorial",
            "n:", "5", false, "",
            "Calcular n! = n × (n-1) × ... × 1, con 0! = 1.",
            "Si n ≤ 1, devuelve 1.",
            "Si n > 1, devuelve n * Factorial(n - 1). Cada llamada reduce n en 1, así que se acerca al caso base.",
            "Un ciclo for acumula el producto de 2 a n en una variable.",
            "Ambas versiones O(n).",
            "Iterativa O(1); recursiva O(n) por las n llamadas apiladas."),

        new ExerciseInfo(
            "Fibonacci",
            "n:", "6", false, "",
            "Calcular el n-ésimo término de la serie 0, 1, 1, 2, 3, 5, 8, ...",
            "Si n < 2, devuelve n (F(0) = 0 y F(1) = 1).",
            "Si n ≥ 2, devuelve Fibonacci(n - 1) + Fibonacci(n - 2). Hay dos llamadas por nivel, por eso se repiten cálculos.",
            "Dos variables guardan los dos últimos términos y se avanzan n - 1 veces.",
            "Iterativa O(n); recursiva O(φ^n), aproximadamente O(2^n).",
            "Iterativa O(1); recursiva O(n) de profundidad de pila."),

        new ExerciseInfo(
            "Torres de Hanói",
            "Discos:", "3", false, "",
            "Mover n discos de la torre A a la torre C usando B, sin poner nunca un disco grande sobre uno pequeño.",
            "Si hay 1 disco, se mueve directamente de origen a destino.",
            "Mover n-1 discos de origen a auxiliar, mover el disco n de origen a destino y mover los n-1 discos de auxiliar a destino.",
            "Se simulan las tres torres con pilas y se repite un ciclo de 3 movimientos legales: origen-destino, origen-auxiliar, auxiliar-destino (con n par se intercambian destino y auxiliar).",
            "Ambas versiones O(2^n): hacen 2^n - 1 movimientos.",
            "Recursiva O(n) de profundidad de pila; iterativa O(n) por los discos guardados en las torres (sin contar la lista de movimientos)."),

        new ExerciseInfo(
            "Suma de un arreglo",
            "Valores:", "4, 8, 15, 16, 23, 42", false, "",
            "Sumar todos los elementos de un arreglo de enteros.",
            "Si el índice llegó al final del arreglo (no quedan elementos), devuelve 0.",
            "Devuelve arreglo[i] + Suma(i + 1): el primer elemento más la suma del resto.",
            "Un ciclo recorre el arreglo y acumula la suma en una variable long.",
            "Ambas versiones O(n).",
            "Iterativa O(1); recursiva O(n) porque hay una llamada por elemento."),

        new ExerciseInfo(
            "Búsqueda lineal",
            "Valores:", "4, 8, 15, 16, 23, 42", true, "16",
            "Devolver el índice de la primera aparición de un valor en el arreglo, o -1 si no está.",
            "Dos casos base: si el índice llegó al final, devuelve -1; si arreglo[i] es el valor buscado, devuelve i.",
            "Si no es ninguno de los casos base, devuelve Buscar(i + 1).",
            "Un ciclo revisa cada posición y regresa en cuanto encuentra el valor.",
            "Ambas versiones O(n) en el peor caso; O(1) si está en la primera posición.",
            "Iterativa O(1); recursiva O(n) en el peor caso por las llamadas apiladas.")
    };
}
