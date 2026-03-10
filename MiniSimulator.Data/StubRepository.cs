namespace MiniSimulator.Data;

internal class StubRepository : ITeamRepository
{
    public TeamData[] GetTeams() =>
    [
        new TeamData("Team A", 90, 60, 75, true),
        new TeamData("Team B", 40, 80, 95, true),
        new TeamData("Team C", 50, 15, 65, false),
        new TeamData("Team D", 70, 95, 90, false)
    ];
}