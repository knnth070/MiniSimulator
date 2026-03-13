namespace MiniSimulator.Domain;

public class RankingComparer(IEnumerable<Match> matches) : IComparer<RankingItem>
{
    public int Compare(RankingItem x, RankingItem y)
    {
        var pointsComparison = x.Points.CompareTo(y.Points);
        if (pointsComparison != 0) return pointsComparison;

        var diffComparison = (x.For - x.Against).CompareTo(y.For - y.Against);
        if (diffComparison != 0) return diffComparison;

        var forComparison = x.For.CompareTo(y.For);
        if (forComparison != 0) return forComparison;

        // This tiebreaker conforms to the assignment, but if diffComparison
        // and forComparison don't break the tie, neither will this one
        var againstComparison = x.Against.CompareTo(y.Against);
        // fewest goals against is better, so invert result
        if (againstComparison != 0) return -1 * againstComparison;

        var match = matches
            .Single(m => m.HomeTeam.Name == x.Team && m.AwayTeam.Name == y.Team ||
                         m.AwayTeam.Name == x.Team && m.HomeTeam.Name == y.Team);
        if (match.HomeGoals > match.AwayGoals)
        {
            return x.Team == match.HomeTeam.Name ? 1 : -1;
        }

        if (match.AwayGoals > match.HomeGoals)
        {
            return x.Team == match.AwayTeam.Name ? 1 : -1;
        }

        return 0;
    }
}