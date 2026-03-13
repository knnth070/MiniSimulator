namespace MiniSimulator.Domain;

public class Group(List<Match> matches)
{
    public List<Match> Matches { get; } = matches;
}