namespace MiniSimulator.Domain;

public class Match(Team homeTeam, Team awayTeam, int round)
{
    public Team HomeTeam { get; } = homeTeam;
    public Team AwayTeam { get; } = awayTeam;
    public int Round { get; } = round;
    public bool IsPlayed { get; set; } = false;
    public int HomeGoals { get; set; }
    public int AwayGoals { get; set; }

    public override string ToString() =>
        $"R{Round}: {HomeTeam.Name} - {AwayTeam.Name}" + (IsPlayed ? $": {HomeGoals} -  {AwayGoals}" : "");
}