namespace MiniSimulator.Domain;

public static class RankingGenerator
{
    private const int PointsForWin = 3;
    private const int PointsForDraw = 1;

    public static List<RankingItem> GetRanking(IEnumerable<Team> teams, Group group)
    {
        var ranking = new List<RankingItem>();

        foreach (var team in teams)
        {
            ranking.Add(
                group.Matches
                    .Where(m => m.HomeTeam.Name == team.Name || m.AwayTeam.Name == team.Name)
                    .Select(m =>
                    {
                        var home = m.HomeTeam.Name == team.Name;
                        var goals = home ? m.HomeGoals : m.AwayGoals;
                        var goalsAgainst = home ? m.AwayGoals : m.HomeGoals;

                        var isWin = (goals > goalsAgainst);
                        var isDraw = goals == goalsAgainst;
                        var isLoss = goals < goalsAgainst;

                        return new RankingItem
                        {
                            Team = team.Name,
                            Played = 1,
                            Win = isWin ? 1 : 0,
                            Draw = isDraw ? 1 : 0,
                            Loss = isLoss ? 1 : 0,
                            For = goals,
                            Against = goalsAgainst,
                            Points = isWin ? PointsForWin : isDraw ? PointsForDraw : 0
                        };
                    })
                    .GroupBy(r => r.Team)
                    .Select(g =>
                        new RankingItem
                        {
                            Team = g.Key,
                            Played = g.Count(),
                            Win = g.Sum(x => x.Win),
                            Draw = g.Sum(x => x.Draw),
                            Loss = g.Sum(x => x.Loss),
                            For = g.Sum(x => x.For),
                            Against = g.Sum(x => x.Against),
                            Points = g.Sum(x => x.Points)
                        })
                    .Single());
        }

        return ranking
            .OrderByDescending(x => x, new RankingComparer(group.Matches))
            .Select((r, i) => r with { Position = i + 1 })
            .ToList();
    }
}