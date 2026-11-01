namespace PracticaU2Act2;

/// <summary>One move of the Towers of Hanoi: which disk goes from which peg to which peg.</summary>
public readonly record struct HanoiMove(int Disk, char From, char To)
{
    public override string ToString() => $"Disco {Disk}: {From} -> {To}";
}
