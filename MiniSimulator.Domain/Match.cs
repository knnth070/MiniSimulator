namespace MiniSimulator.Domain;

public class Match(Team homeTeam, Team awayTeam)
{
    public Team HomeTeam { get; } = homeTeam;
    public Team AwayTeam { get; } = awayTeam;
    public bool IsPlayed { get; set; } = false;
    public int HomeGoals { get; set; }
    public int AwayGoals { get; set; }

    public override string ToString() =>
        $"{HomeTeam.Name} - {AwayTeam.Name}" + (IsPlayed ? $": {HomeGoals} -  {AwayGoals}" : "");
}