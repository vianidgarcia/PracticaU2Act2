namespace PracticaU2Act2;

/// <summary>
/// Catalog of exercises. Every exercise has an iterative version and a recursive version.
/// In each recursive method the base case and the recursive case are marked with comments.
/// The optional CallTracer only observes the calls; it never changes the result.
/// </summary>
public static class RecursiveCatalog
{
    // Limits chosen so results fit in a long and the UI does not freeze.
    public const int MaxFactorial = 20;            // 21! overflows a long
    public const int MaxFibonacciIterative = 92;   // F(93) overflows a long
    public const int MaxFibonacciRecursive = 40;   // the naive version makes ~331 million calls at 40
    public const int MinHanoiDisks = 1;
    public const int MaxHanoiDisks = 20;           // 2^20 - 1 moves
    public const int MaxArrayLength = 5000;        // one stack frame per element in the recursive versions

    // ------------------------------------------------------------------ Factorial

    public static long FactorialIterative(int n)
    {
        ValidateFactorial(n);

        long result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    public static long FactorialRecursive(int n, CallTracer? tracer = null)
    {
        ValidateFactorial(n);
        return FactorialStep(n, 1, tracer);
    }

    private static long FactorialStep(int n, int depth, CallTracer? tracer)
    {
        tracer?.Enter(depth);
        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"-> Factorial({n})");
        }

        long result;
        if (n <= 1)
        {
            result = 1;                                        // BASE CASE: 0! = 1! = 1
        }
        else
        {
            result = n * FactorialStep(n - 1, depth + 1, tracer); // RECURSIVE CASE: n! = n * (n-1)!
        }

        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"<= Factorial({n}) = {result}");
        }

        return result;
    }

    private static void ValidateFactorial(int n)
    {
        if (n < 0 || n > MaxFactorial)
        {
            throw new ArgumentOutOfRangeException(null, ErrorMessages.FactorialRange(MaxFactorial));
        }
    }

    // ------------------------------------------------------------------ Fibonacci

    public static long FibonacciIterative(int n)
    {
        if (n < 0 || n > MaxFibonacciIterative)
        {
            throw new ArgumentOutOfRangeException(null, ErrorMessages.FibonacciIterativeRange(MaxFibonacciIterative));
        }

        if (n < 2)
        {
            return n;
        }

        long previous = 0;
        long current = 1;
        for (int i = 2; i <= n; i++)
        {
            long next = previous + current;
            previous = current;
            current = next;
        }

        return current;
    }

    public static long FibonacciRecursive(int n, CallTracer? tracer = null)
    {
        if (n < 0 || n > MaxFibonacciRecursive)
        {
            throw new ArgumentOutOfRangeException(null, ErrorMessages.FibonacciRecursiveRange(MaxFibonacciRecursive));
        }

        return FibonacciStep(n, 1, tracer);
    }

    private static long FibonacciStep(int n, int depth, CallTracer? tracer)
    {
        tracer?.Enter(depth);
        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"-> Fib({n})");
        }

        long result;
        if (n < 2)
        {
            result = n;                                        // BASE CASE: F(0) = 0, F(1) = 1
        }
        else
        {
            // RECURSIVE CASE: F(n) = F(n-1) + F(n-2)
            result = FibonacciStep(n - 1, depth + 1, tracer) + FibonacciStep(n - 2, depth + 1, tracer);
        }

        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"<= Fib({n}) = {result}");
        }

        return result;
    }

    // ------------------------------------------------------------------ Towers of Hanoi

    /// <summary>
    /// Iterative solution without recursion. Rule: the smallest disk moves one peg in a fixed
    /// direction every 3 moves in a cycle, and the other legal move involves the two other pegs.
    /// The native Stack&lt;int&gt; is used only to represent the three pegs.
    /// </summary>
    public static List<HanoiMove> HanoiIterative(int disks)
    {
        ValidateHanoi(disks);

        var pegs = new Stack<int>[] { new(), new(), new() };
        for (int disk = disks; disk >= 1; disk--)
        {
            pegs[0].Push(disk);
        }

        char[] names = { 'A', 'B', 'C' };
        int source = 0;
        int target = 2;
        int auxiliary = 1;
        if (disks % 2 == 0)
        {
            (target, auxiliary) = (auxiliary, target); // with an even number of disks the cycle goes the other way
        }

        long total = (1L << disks) - 1;
        var moves = new List<HanoiMove>((int)total);
        for (long i = 1; i <= total; i++)
        {
            switch (i % 3)
            {
                case 1:
                    moves.Add(MakeLegalMove(pegs, names, source, target));
                    break;
                case 2:
                    moves.Add(MakeLegalMove(pegs, names, source, auxiliary));
                    break;
                default:
                    moves.Add(MakeLegalMove(pegs, names, auxiliary, target));
                    break;
            }
        }

        return moves;
    }

    /// <summary>Moves the smaller top disk between two pegs; if one peg is empty, moves into it.</summary>
    private static HanoiMove MakeLegalMove(Stack<int>[] pegs, char[] names, int p, int q)
    {
        int from;
        int to;
        if (pegs[p].Count == 0)
        {
            from = q;
            to = p;
        }
        else if (pegs[q].Count == 0)
        {
            from = p;
            to = q;
        }
        else if (pegs[p].Peek() < pegs[q].Peek())
        {
            from = p;
            to = q;
        }
        else
        {
            from = q;
            to = p;
        }

        int disk = pegs[from].Pop();
        pegs[to].Push(disk);
        return new HanoiMove(disk, names[from], names[to]);
    }

    public static List<HanoiMove> HanoiRecursive(int disks, CallTracer? tracer = null)
    {
        ValidateHanoi(disks);

        var moves = new List<HanoiMove>((int)((1L << disks) - 1));
        HanoiStep(disks, 'A', 'C', 'B', moves, 1, tracer);
        return moves;
    }

    private static void HanoiStep(int disks, char from, char to, char via, List<HanoiMove> moves, int depth, CallTracer? tracer)
    {
        tracer?.Enter(depth);
        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"-> Hanoi({disks}, {from} -> {to}, auxiliar {via})");
        }

        if (disks == 1)
        {
            // BASE CASE: a single disk moves directly.
            moves.Add(new HanoiMove(1, from, to));
            if (tracer is { Logging: true })
            {
                tracer.Log(depth, $"   mover disco 1: {from} -> {to}");
            }

            return;
        }

        // RECURSIVE CASE: move n-1 disks out of the way, move the largest, bring the n-1 disks on top of it.
        HanoiStep(disks - 1, from, via, to, moves, depth + 1, tracer);
        moves.Add(new HanoiMove(disks, from, to));
        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"   mover disco {disks}: {from} -> {to}");
        }

        HanoiStep(disks - 1, via, to, from, moves, depth + 1, tracer);
    }

    private static void ValidateHanoi(int disks)
    {
        if (disks < MinHanoiDisks || disks > MaxHanoiDisks)
        {
            throw new ArgumentOutOfRangeException(null, ErrorMessages.HanoiRange(MinHanoiDisks, MaxHanoiDisks));
        }
    }

    // ------------------------------------------------------------------ Sum of an array

    public static long ArraySumIterative(int[] values)
    {
        ValidateArray(values);

        long sum = 0;
        foreach (int value in values)
        {
            sum += value;
        }

        return sum;
    }

    public static long ArraySumRecursive(int[] values, CallTracer? tracer = null)
    {
        ValidateArray(values);
        return ArraySumStep(values, 0, 1, tracer);
    }

    private static long ArraySumStep(int[] values, int index, int depth, CallTracer? tracer)
    {
        tracer?.Enter(depth);
        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"-> Suma(indice {index})");
        }

        long result;
        if (index >= values.Length)
        {
            result = 0;                                                      // BASE CASE: no elements left
        }
        else
        {
            result = values[index] + ArraySumStep(values, index + 1, depth + 1, tracer); // RECURSIVE CASE
        }

        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"<= Suma(indice {index}) = {result}");
        }

        return result;
    }

    // ------------------------------------------------------------------ Linear search

    /// <summary>Returns the index of the first occurrence of target, or -1 when it is not in the array.</summary>
    public static int LinearSearchIterative(int[] values, int target)
    {
        ValidateArray(values);

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == target)
            {
                return i;
            }
        }

        return -1;
    }

    public static int LinearSearchRecursive(int[] values, int target, CallTracer? tracer = null)
    {
        ValidateArray(values);
        return LinearSearchStep(values, target, 0, 1, tracer);
    }

    private static int LinearSearchStep(int[] values, int target, int index, int depth, CallTracer? tracer)
    {
        tracer?.Enter(depth);
        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"-> Buscar(indice {index})");
        }

        int result;
        if (index >= values.Length)
        {
            result = -1;                                                     // BASE CASE 1: ran out of elements
        }
        else if (values[index] == target)
        {
            result = index;                                                  // BASE CASE 2: found it
        }
        else
        {
            result = LinearSearchStep(values, target, index + 1, depth + 1, tracer); // RECURSIVE CASE
        }

        if (tracer is { Logging: true })
        {
            tracer.Log(depth, $"<= Buscar(indice {index}) = {result}");
        }

        return result;
    }

    private static void ValidateArray(int[] values)
    {
        if (values is null)
        {
            throw new ArgumentNullException(null, ErrorMessages.NullArray);
        }
    }
}
