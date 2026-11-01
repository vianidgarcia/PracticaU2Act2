using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace PracticaU2Act2;

/// <summary>
/// Runs one exercise with both versions (iterative and recursive), compares the results and
/// builds the text report shown in the window. It has no controls, so it can run on another thread.
/// </summary>
public sealed class ExerciseRunner
{
    private const int MaxMovesShown = 64;

    public string Run(int exerciseIndex, string input, string targetText)
    {
        var kind = (ExerciseKind)exerciseIndex;
        ExerciseInfo info = ExerciseCatalogInfo.All[exerciseIndex];

        var sb = new StringBuilder();
        sb.AppendLine($"Ejercicio: {info.Name}");

        switch (kind)
        {
            case ExerciseKind.Factorial:
                RunFactorial(InputParser.ParseInt(input), sb);
                break;
            case ExerciseKind.Fibonacci:
                RunFibonacci(InputParser.ParseInt(input), sb);
                break;
            case ExerciseKind.Hanoi:
                RunHanoi(InputParser.ParseInt(input), sb);
                break;
            case ExerciseKind.ArraySum:
                RunArraySum(InputParser.ParseIntList(input), sb);
                break;
            case ExerciseKind.LinearSearch:
                RunLinearSearch(InputParser.ParseIntList(input), InputParser.ParseInt(targetText), sb);
                break;
            default:
                throw new ArgumentOutOfRangeException(null, "Ejercicio desconocido.");
        }

        return sb.ToString();
    }

    private static void RunFactorial(int n, StringBuilder sb)
    {
        var iterative = Measure(() => RecursiveCatalog.FactorialIterative(n));
        var recursive = Measure(() => RecursiveCatalog.FactorialRecursive(n));
        var tracer = new CallTracer(keepLines: true);
        RecursiveCatalog.FactorialRecursive(n, tracer);

        sb.AppendLine($"Entrada: n = {n}");
        AppendComparison(sb, Format(iterative.Value), iterative.Ms, Format(recursive.Value), recursive.Ms,
            iterative.Value == recursive.Value);
        AppendTrace(sb, tracer, "Prueba de escritorio (la sangría indica la profundidad; '->' llamada, '<=' retorno):");
    }

    private static void RunFibonacci(int n, StringBuilder sb)
    {
        var iterative = Measure(() => RecursiveCatalog.FibonacciIterative(n));
        var recursive = Measure(() => RecursiveCatalog.FibonacciRecursive(n));
        var tracer = new CallTracer(keepLines: true);
        RecursiveCatalog.FibonacciRecursive(n, tracer);

        sb.AppendLine($"Entrada: n = {n}");
        AppendComparison(sb, Format(iterative.Value), iterative.Ms, Format(recursive.Value), recursive.Ms,
            iterative.Value == recursive.Value);
        AppendTrace(sb, tracer, "Prueba de escritorio (la sangría indica la profundidad; '->' llamada, '<=' retorno):");
    }

    private static void RunHanoi(int disks, StringBuilder sb)
    {
        var iterative = Measure(() => RecursiveCatalog.HanoiIterative(disks));
        var recursive = Measure(() => RecursiveCatalog.HanoiRecursive(disks));
        var tracer = new CallTracer(keepLines: true);
        RecursiveCatalog.HanoiRecursive(disks, tracer);

        bool same = iterative.Value.SequenceEqual(recursive.Value);
        sb.AppendLine($"Entrada: {disks} discos, de A a C usando B");
        sb.AppendLine($"Movimientos esperados: 2^{disks} - 1 = {Format(recursive.Value.Count)}");
        AppendComparison(sb, $"{Format(iterative.Value.Count)} movimientos", iterative.Ms,
            $"{Format(recursive.Value.Count)} movimientos", recursive.Ms, same, "Misma secuencia de movimientos");

        sb.AppendLine();
        int shown = Math.Min(MaxMovesShown, recursive.Value.Count);
        sb.AppendLine($"Movimientos (primeros {shown}):");
        for (int i = 0; i < shown; i++)
        {
            sb.AppendLine($"{i + 1,4}. {recursive.Value[i]}");
        }

        if (recursive.Value.Count > shown)
        {
            sb.AppendLine("...");
        }

        AppendTrace(sb, tracer, "Prueba de escritorio (la sangría indica la profundidad; '->' llamada):");
    }

    private static void RunArraySum(int[] values, StringBuilder sb)
    {
        var iterative = Measure(() => RecursiveCatalog.ArraySumIterative(values));
        var recursive = Measure(() => RecursiveCatalog.ArraySumRecursive(values));
        var tracer = new CallTracer(keepLines: true);
        RecursiveCatalog.ArraySumRecursive(values, tracer);

        sb.AppendLine($"Entrada: {values.Length} valores");
        AppendComparison(sb, Format(iterative.Value), iterative.Ms, Format(recursive.Value), recursive.Ms,
            iterative.Value == recursive.Value);
        AppendTrace(sb, tracer, "Prueba de escritorio (la sangría indica la profundidad; '->' llamada, '<=' retorno):");
    }

    private static void RunLinearSearch(int[] values, int target, StringBuilder sb)
    {
        var iterative = Measure(() => RecursiveCatalog.LinearSearchIterative(values, target));
        var recursive = Measure(() => RecursiveCatalog.LinearSearchRecursive(values, target));
        var tracer = new CallTracer(keepLines: true);
        RecursiveCatalog.LinearSearchRecursive(values, target, tracer);

        sb.AppendLine($"Entrada: {values.Length} valores, buscando {target}");
        AppendComparison(sb, DescribeIndex(iterative.Value), iterative.Ms, DescribeIndex(recursive.Value), recursive.Ms,
            iterative.Value == recursive.Value);
        AppendTrace(sb, tracer, "Prueba de escritorio (la sangría indica la profundidad; '->' llamada, '<=' retorno):");
    }

    // ------------------------------------------------------------------ helpers

    /// <summary>Runs the action once and returns its value and the elapsed milliseconds (single run, reference only).</summary>
    private static (T Value, double Ms) Measure<T>(Func<T> action)
    {
        var stopwatch = Stopwatch.StartNew();
        T value = action();
        stopwatch.Stop();
        return (value, stopwatch.Elapsed.TotalMilliseconds);
    }

    private static void AppendComparison(StringBuilder sb, string iterativeText, double iterativeMs,
        string recursiveText, double recursiveMs, bool same, string sameLabel = "Resultados iguales")
    {
        sb.AppendLine();
        sb.AppendLine($"Versión iterativa:  {iterativeText}   ({FormatMs(iterativeMs)} ms)");
        sb.AppendLine($"Versión recursiva:  {recursiveText}   ({FormatMs(recursiveMs)} ms)");
        sb.AppendLine($"{sameLabel}: {(same ? "sí" : "NO")}");
        sb.AppendLine("(Tiempos de una sola ejecución, solo como referencia.)");
    }

    private static void AppendTrace(StringBuilder sb, CallTracer tracer, string header)
    {
        sb.AppendLine();
        sb.AppendLine($"Llamadas recursivas: {Format(tracer.Calls)}");
        sb.AppendLine($"Profundidad máxima de la pila: {tracer.MaxDepth} llamadas activas a la vez");
        sb.AppendLine();
        sb.AppendLine(header);
        foreach (string line in tracer.Lines)
        {
            sb.AppendLine(line);
        }

        if (tracer.Truncated)
        {
            sb.AppendLine("... (traza recortada)");
        }
    }

    private static string DescribeIndex(int index) => index < 0 ? "-1 (no encontrado)" : $"índice {index}";

    private static string Format(long value) => value.ToString("N0", CultureInfo.InvariantCulture);

    private static string FormatMs(double ms) => ms.ToString("0.0000", CultureInfo.InvariantCulture);
}
