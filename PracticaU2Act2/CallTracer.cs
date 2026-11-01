namespace PracticaU2Act2;

/// <summary>
/// Records what a recursive method does: how many calls it makes, how deep the
/// call stack gets and (optionally) a line per call/return for the desk test.
/// Depth starts at 1 for the first call, so MaxDepth is the number of frames
/// that are active at the same time at the deepest point.
/// </summary>
public sealed class CallTracer
{
    private readonly List<string> _lines = new();
    private readonly int _maxLines;

    public CallTracer(bool keepLines, int maxLines = 300)
    {
        KeepLines = keepLines;
        _maxLines = maxLines;
    }

    /// <summary>True when the caller wants the text trace and not only the counters.</summary>
    public bool KeepLines { get; }

    /// <summary>Total number of recursive calls made, including the first one.</summary>
    public long Calls { get; private set; }

    /// <summary>Largest number of calls waiting on the stack at the same time.</summary>
    public int MaxDepth { get; private set; }

    /// <summary>True once the trace reached its line limit and later lines were dropped.</summary>
    public bool Truncated { get; private set; }

    /// <summary>
    /// Callers check this before building a trace string, so a huge run
    /// (for example Fibonacci(40)) does not create millions of strings.
    /// </summary>
    public bool Logging => KeepLines && !Truncated;

    public IReadOnlyList<string> Lines => _lines;

    /// <summary>Counts one call that is running at the given depth (first call = 1).</summary>
    public void Enter(int depth)
    {
        Calls++;
        if (depth > MaxDepth)
        {
            MaxDepth = depth;
        }
    }

    /// <summary>Adds one trace line, indented two spaces per level below the first.</summary>
    public void Log(int depth, string text)
    {
        if (_lines.Count >= _maxLines)
        {
            Truncated = true;
            return;
        }

        _lines.Add(new string(' ', (depth - 1) * 2) + text);
    }
}
