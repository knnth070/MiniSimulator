namespace MiniSimulator.Domain;

public readonly struct RankingItem
{
    public int Position { get; init; }
    public string Team { get; init; }
    public int Played { get; init; }
    public int Win { get; init; }
    public int Draw { get; init; }
    public int Loss { get; init; }
    public int For { get; init; }
    public int Against { get; init; }
    public int Points { get; init; }

    public override string ToString() =>
        $"{Position, 5} {Team,-10} {Played,-8} {Win,-6} {Draw,-6} {Loss, -6} {For,-5} {Against, -9} {For - Against,-6} {Points,-6}";
}