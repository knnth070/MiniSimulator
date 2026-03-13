using MiniSimulator.Domain;

namespace MiniSimulator.Test;

[TestClass]
public class RankingComparerTest
{
    private const string FirstTeam = "First Team";
    private const string OtherTeam = "Other Team";

    private readonly Match[] _matches =
    [
        new Match(new Team(FirstTeam, 1, 1, 1, false),
            new Team(OtherTeam, 1, 1, 1, false), 1)
        {
            HomeGoals = 2, AwayGoals = 1, IsPlayed = true
        }
    ];

    [TestMethod]
    public void MostPointsRankHigher()
    {
        var ranking1 = new RankingItem
        {
            Team = FirstTeam,
            Points = 10,
            For = 5,
            Against = 2,
        };
        var ranking2 = new RankingItem
        {
            Team = OtherTeam,
            Points = 8,
            For = 5,
            Against = 0,
        };

        var sut = new RankingComparer(_matches);
        var result1 = sut.Compare(ranking1, ranking2);
        var result2 = sut.Compare(ranking2, ranking1);
        Assert.AreEqual(1, result1);
        Assert.AreEqual(-1, result2);
    }

    [TestMethod]
    public void GoalDifferenceIsFirstTiebreaker()
    {
        var ranking1 = new RankingItem
        {
            Team = FirstTeam,
            Points = 10,
            For = 5,
            Against = 0,
        };
        var ranking2 = new RankingItem
        {
            Team = OtherTeam,
            Points = 10,
            For = 5,
            Against = 3,
        };

        var sut = new RankingComparer(_matches);
        var result1 = sut.Compare(ranking1, ranking2);
        var result2 = sut.Compare(ranking2, ranking1);
        Assert.AreEqual(1, result1);
        Assert.AreEqual(-1, result2);
    }

    [TestMethod]
    public void GoalsForIsSecondTiebreaker()
    {
        var ranking1 = new RankingItem
        {
            Team = FirstTeam,
            Points = 10,
            For = 9,
            Against = 3,
        };
        var ranking2 = new RankingItem
        {
            Team = OtherTeam,
            Points = 10,
            For = 7,
            Against = 1,
        };

        var sut = new RankingComparer(_matches);
        var result1 = sut.Compare(ranking1, ranking2);
        var result2 = sut.Compare(ranking2, ranking1);
        Assert.AreEqual(1, result1);
        Assert.AreEqual(-1, result2);
    }

    // Goals against will not break the tie when difference and goals for
    // don't, so this code path cannot be covered

    [TestMethod]
    public void HeadToHeadIsLastTiebreaker()
    {
        var ranking1 = new RankingItem
        {
            Team = FirstTeam,
            Points = 7,
            For = 5,
            Against = 2,
        };
        var ranking2 = new RankingItem
        {
            Team = OtherTeam,
            Points = 7,
            For = 5,
            Against = 2,
        };

        var sut = new RankingComparer(_matches);
        var result1 = sut.Compare(ranking1, ranking2);
        var result2 = sut.Compare(ranking2, ranking1);
        Assert.AreEqual(1, result1);
        Assert.AreEqual(-1, result2);
    }
}