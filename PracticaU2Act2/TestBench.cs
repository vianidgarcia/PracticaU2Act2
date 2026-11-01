using System.Text;

namespace PracticaU2Act2;

public sealed record TestResult(int Number, string Name, bool Passed, string Detail);

/// <summary>
/// Automatic test cases for the whole catalog. Every case returns null when it passes,
/// or a text describing what was different when it fails.
/// </summary>
public static class TestBench
{
    public static List<TestResult> RunAll()
    {
        var results = new List<TestResult>();

        void Add(string name, Func<string?> check)
        {
            string? failure;
            try
            {
                failure = check();
            }
            catch (Exception ex)
            {
                failure = $"excepción inesperada {ex.GetType().Name}: {ex.Message}";
            }

            results.Add(new TestResult(results.Count + 1, name, failure is null, failure ?? "OK"));
        }

        Add("Factorial: valores conocidos (0!, 1!, 5!, 10!, 20!) en ambas versiones", FactorialKnownValues);
        Add("Factorial: rechaza -1 y 21 con el mensaje en español", FactorialInvalid);
        Add("Fibonacci: primeros 15 términos en ambas versiones", FibonacciFirstTerms);
        Add("Fibonacci: F(30) recursivo = iterativo y F(92) iterativo", FibonacciLargerValues);
        Add("Fibonacci: rechaza valores fuera de rango en cada versión", FibonacciInvalid);
        Add("Hanói: cantidad de movimientos = 2^n - 1 (n = 1 a 12) en ambas versiones", HanoiMoveCount);
        Add("Hanói: ambas versiones producen la misma secuencia (n = 1 a 10)", HanoiSameSequence);
        Add("Hanói: la secuencia es legal y termina con todos los discos en C (n = 8)", HanoiLegalSequence);
        Add("Hanói: rechaza 0 y 21 discos", HanoiInvalid);
        Add("Suma de arreglo: vacío, un elemento, negativos y 1 a 100", ArraySumKnownValues);
        Add("Suma de arreglo: 1 000 valores aleatorios contra una referencia independiente", ArraySumRandom);
        Add("Arreglo nulo: las cuatro funciones lo rechazan", NullArrayRejected);
        Add("Búsqueda lineal: primero, último, repetido, ausente y arreglo vacío", LinearSearchCases);
        Add("Traza: Factorial(4) = 4 llamadas y Fib(5) = 15 llamadas", TraceFactorialAndFibonacci);
        Add("Traza: Hanói(5) = 31 llamadas y profundidad 5", TraceHanoi);
        Add("Traza: suma de 10 elementos y búsquedas (11 llamadas, 11 y 4)", TraceArrayFunctions);

        return results;
    }

    public static string FormatReport(IReadOnlyList<TestResult> results)
    {
        var sb = new StringBuilder();
        foreach (TestResult result in results)
        {
            sb.AppendLine($"[{(result.Passed ? "OK" : "FALLO")}] {result.Number,2}. {result.Name}");
            if (!result.Passed)
            {
                sb.AppendLine($"        {result.Detail}");
            }
        }

        sb.AppendLine();
        sb.AppendLine($"{results.Count(r => r.Passed)} de {results.Count} casos aprobados");
        return sb.ToString();
    }

    // ------------------------------------------------------------------ Factorial

    private static string? FactorialKnownValues()
    {
        (int N, long Expected)[] cases = { (0, 1), (1, 1), (5, 120), (10, 3628800), (20, 2432902008176640000L) };
        foreach ((int n, long expected) in cases)
        {
            long iterative = RecursiveCatalog.FactorialIterative(n);
            long recursive = RecursiveCatalog.FactorialRecursive(n);
            if (iterative != expected || recursive != expected)
            {
                return $"n={n}: esperado {expected}, iterativo {iterative}, recursivo {recursive}";
            }
        }

        return null;
    }

    private static string? FactorialInvalid()
    {
        string message = ErrorMessages.FactorialRange(RecursiveCatalog.MaxFactorial);
        return FirstFailure(
            ExpectError(() => RecursiveCatalog.FactorialIterative(-1), message),
            ExpectError(() => RecursiveCatalog.FactorialRecursive(-1), message),
            ExpectError(() => RecursiveCatalog.FactorialIterative(21), message),
            ExpectError(() => RecursiveCatalog.FactorialRecursive(21), message));
    }

    // ------------------------------------------------------------------ Fibonacci

    private static string? FibonacciFirstTerms()
    {
        long[] expected = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 };
        for (int n = 0; n < expected.Length; n++)
        {
            long iterative = RecursiveCatalog.FibonacciIterative(n);
            long recursive = RecursiveCatalog.FibonacciRecursive(n);
            if (iterative != expected[n] || recursive != expected[n])
            {
                return $"n={n}: esperado {expected[n]}, iterativo {iterative}, recursivo {recursive}";
            }
        }

        return null;
    }

    private static string? FibonacciLargerValues()
    {
        long iterative30 = RecursiveCatalog.FibonacciIterative(30);
        long recursive30 = RecursiveCatalog.FibonacciRecursive(30);
        if (iterative30 != 832040 || recursive30 != 832040)
        {
            return $"F(30): esperado 832040, iterativo {iterative30}, recursivo {recursive30}";
        }

        long iterative92 = RecursiveCatalog.FibonacciIterative(92);
        return iterative92 == 7540113804746346429L ? null : $"F(92): esperado 7540113804746346429, obtenido {iterative92}";
    }

    private static string? FibonacciInvalid()
    {
        string iterativeMessage = ErrorMessages.FibonacciIterativeRange(RecursiveCatalog.MaxFibonacciIterative);
        string recursiveMessage = ErrorMessages.FibonacciRecursiveRange(RecursiveCatalog.MaxFibonacciRecursive);
        return FirstFailure(
            ExpectError(() => RecursiveCatalog.FibonacciIterative(-1), iterativeMessage),
            ExpectError(() => RecursiveCatalog.FibonacciIterative(93), iterativeMessage),
            ExpectError(() => RecursiveCatalog.FibonacciRecursive(-1), recursiveMessage),
            ExpectError(() => RecursiveCatalog.FibonacciRecursive(41), recursiveMessage));
    }

    // ------------------------------------------------------------------ Hanoi

    private static string? HanoiMoveCount()
    {
        for (int n = 1; n <= 12; n++)
        {
            int expected = (1 << n) - 1;
            int iterative = RecursiveCatalog.HanoiIterative(n).Count;
            int recursive = RecursiveCatalog.HanoiRecursive(n).Count;
            if (iterative != expected || recursive != expected)
            {
                return $"n={n}: esperado {expected}, iterativo {iterative}, recursivo {recursive}";
            }
        }

        return null;
    }

    private static string? HanoiSameSequence()
    {
        for (int n = 1; n <= 10; n++)
        {
            List<HanoiMove> iterative = RecursiveCatalog.HanoiIterative(n);
            List<HanoiMove> recursive = RecursiveCatalog.HanoiRecursive(n);
            if (!iterative.SequenceEqual(recursive))
            {
                return $"n={n}: las secuencias son distintas";
            }
        }

        return null;
    }

    private static string? HanoiLegalSequence()
    {
        const int disks = 8;
        foreach ((string label, List<HanoiMove> moves) in new[]
                 {
                     ("recursiva", RecursiveCatalog.HanoiRecursive(disks)),
                     ("iterativa", RecursiveCatalog.HanoiIterative(disks))
                 })
        {
            var pegs = new Stack<int>[] { new(), new(), new() };
            for (int disk = disks; disk >= 1; disk--)
            {
                pegs[0].Push(disk);
            }

            for (int i = 0; i < moves.Count; i++)
            {
                HanoiMove move = moves[i];
                Stack<int> from = pegs[move.From - 'A'];
                Stack<int> to = pegs[move.To - 'A'];
                if (from.Count == 0 || from.Peek() != move.Disk)
                {
                    return $"versión {label}, movimiento {i + 1}: el disco {move.Disk} no está arriba de {move.From}";
                }

                if (to.Count > 0 && to.Peek() < move.Disk)
                {
                    return $"versión {label}, movimiento {i + 1}: disco grande sobre uno pequeño";
                }

                to.Push(from.Pop());
            }

            if (pegs[2].Count != disks || pegs[0].Count != 0 || pegs[1].Count != 0)
            {
                return $"versión {label}: al final no están todos los discos en C";
            }
        }

        return null;
    }

    private static string? HanoiInvalid()
    {
        string message = ErrorMessages.HanoiRange(RecursiveCatalog.MinHanoiDisks, RecursiveCatalog.MaxHanoiDisks);
        return FirstFailure(
            ExpectError(() => RecursiveCatalog.HanoiIterative(0), message),
            ExpectError(() => RecursiveCatalog.HanoiRecursive(0), message),
            ExpectError(() => RecursiveCatalog.HanoiIterative(21), message),
            ExpectError(() => RecursiveCatalog.HanoiRecursive(21), message));
    }

    // ------------------------------------------------------------------ Array sum and search

    private static string? ArraySumKnownValues()
    {
        int[][] arrays = { Array.Empty<int>(), new[] { 7 }, new[] { -5, 3, 2 }, Enumerable.Range(1, 100).ToArray() };
        long[] expected = { 0, 7, 0, 5050 };
        for (int i = 0; i < arrays.Length; i++)
        {
            long iterative = RecursiveCatalog.ArraySumIterative(arrays[i]);
            long recursive = RecursiveCatalog.ArraySumRecursive(arrays[i]);
            if (iterative != expected[i] || recursive != expected[i])
            {
                return $"caso {i + 1}: esperado {expected[i]}, iterativo {iterative}, recursivo {recursive}";
            }
        }

        return null;
    }

    private static string? ArraySumRandom()
    {
        var random = new Random(42);
        int[] values = Enumerable.Range(0, 1000).Select(_ => random.Next(-1000, 1000)).ToArray();
        long reference = values.Sum(v => (long)v); // independent reference (LINQ)
        long iterative = RecursiveCatalog.ArraySumIterative(values);
        long recursive = RecursiveCatalog.ArraySumRecursive(values);
        return iterative == reference && recursive == reference
            ? null
            : $"referencia {reference}, iterativo {iterative}, recursivo {recursive}";
    }

    private static string? NullArrayRejected()
    {
        return FirstFailure(
            ExpectError(() => RecursiveCatalog.ArraySumIterative(null!), ErrorMessages.NullArray),
            ExpectError(() => RecursiveCatalog.ArraySumRecursive(null!), ErrorMessages.NullArray),
            ExpectError(() => RecursiveCatalog.LinearSearchIterative(null!, 1), ErrorMessages.NullArray),
            ExpectError(() => RecursiveCatalog.LinearSearchRecursive(null!, 1), ErrorMessages.NullArray));
    }

    private static string? LinearSearchCases()
    {
        int[] data = { 4, 8, 15, 16, 23, 42, 15 };
        (int Target, int Expected)[] cases = { (4, 0), (42, 5), (15, 2), (99, -1) };
        foreach ((int target, int expected) in cases)
        {
            int iterative = RecursiveCatalog.LinearSearchIterative(data, target);
            int recursive = RecursiveCatalog.LinearSearchRecursive(data, target);
            if (iterative != expected || recursive != expected)
            {
                return $"buscar {target}: esperado {expected}, iterativo {iterative}, recursivo {recursive}";
            }
        }

        int emptyIterative = RecursiveCatalog.LinearSearchIterative(Array.Empty<int>(), 1);
        int emptyRecursive = RecursiveCatalog.LinearSearchRecursive(Array.Empty<int>(), 1);
        return emptyIterative == -1 && emptyRecursive == -1
            ? null
            : $"arreglo vacío: iterativo {emptyIterative}, recursivo {emptyRecursive} (se esperaba -1)";
    }

    // ------------------------------------------------------------------ Tracer

    private static string? TraceFactorialAndFibonacci()
    {
        var factorialTracer = new CallTracer(keepLines: false);
        RecursiveCatalog.FactorialRecursive(4, factorialTracer);
        if (factorialTracer.Calls != 4 || factorialTracer.MaxDepth != 4)
        {
            return $"Factorial(4): llamadas {factorialTracer.Calls} (esperado 4), profundidad {factorialTracer.MaxDepth} (esperado 4)";
        }

        var fibonacciTracer = new CallTracer(keepLines: false);
        RecursiveCatalog.FibonacciRecursive(5, fibonacciTracer);
        return fibonacciTracer.Calls == 15 && fibonacciTracer.MaxDepth == 5
            ? null
            : $"Fib(5): llamadas {fibonacciTracer.Calls} (esperado 15), profundidad {fibonacciTracer.MaxDepth} (esperado 5)";
    }

    private static string? TraceHanoi()
    {
        var tracer = new CallTracer(keepLines: false);
        RecursiveCatalog.HanoiRecursive(5, tracer);
        return tracer.Calls == 31 && tracer.MaxDepth == 5
            ? null
            : $"Hanói(5): llamadas {tracer.Calls} (esperado 31), profundidad {tracer.MaxDepth} (esperado 5)";
    }

    private static string? TraceArrayFunctions()
    {
        int[] data = Enumerable.Range(1, 10).ToArray();

        var sumTracer = new CallTracer(keepLines: false);
        RecursiveCatalog.ArraySumRecursive(data, sumTracer);
        if (sumTracer.Calls != 11 || sumTracer.MaxDepth != 11)
        {
            return $"suma de 10: llamadas {sumTracer.Calls} (esperado 11), profundidad {sumTracer.MaxDepth} (esperado 11)";
        }

        var missingTracer = new CallTracer(keepLines: false);
        RecursiveCatalog.LinearSearchRecursive(data, 99, missingTracer);
        if (missingTracer.Calls != 11)
        {
            return $"búsqueda de un valor ausente: llamadas {missingTracer.Calls} (esperado 11)";
        }

        var foundTracer = new CallTracer(keepLines: false);
        RecursiveCatalog.LinearSearchRecursive(data, 4, foundTracer); // value 4 is at index 3
        return foundTracer.Calls == 4 ? null : $"búsqueda en el índice 3: llamadas {foundTracer.Calls} (esperado 4)";
    }

    // ------------------------------------------------------------------ helpers

    /// <summary>Passes when the action throws an ArgumentException with exactly the expected message.</summary>
    private static string? ExpectError(Action action, string expectedMessage)
    {
        try
        {
            action();
            return "no lanzó ninguna excepción";
        }
        catch (ArgumentException ex)
        {
            return ex.Message == expectedMessage ? null : $"mensaje distinto: \"{ex.Message}\"";
        }
        catch (Exception ex)
        {
            return $"excepción inesperada {ex.GetType().Name}";
        }
    }

    private static string? FirstFailure(params string?[] results) => results.FirstOrDefault(r => r is not null);
}
