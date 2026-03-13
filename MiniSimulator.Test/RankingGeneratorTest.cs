using MiniSimulator.Domain;

namespace MiniSimulator.Test;

[TestClass]
public class RankingGeneratorTest
{
    private readonly Team _teamA = new Team("A", 1, 1, 1, false);
    private readonly Team _teamB = new Team("B", 1, 1, 1, false);
    private readonly Team _teamC = new Team("C", 1, 1, 1, false);
    private readonly Team _teamD = new Team("D", 1, 1, 1, false);

    private Team[] _teams = null!;
    private Group _defaultGroup = null!;
    
    [TestInitialize]
    public void Initialize()
    {
        _teams = [_teamA, _teamB, _teamC, _teamD];
        _defaultGroup = new Group(
            [
                new Match(_teamA, _teamB, 1) { HomeGoals = 3, AwayGoals = 0, IsPlayed = true},
                new Match(_teamA, _teamC, 1) { HomeGoals = 2, AwayGoals = 0, IsPlayed = true},
                new Match(_teamA, _teamD, 1) { HomeGoals = 1, AwayGoals = 0, IsPlayed = true},
                new Match(_teamB, _teamC, 1) { HomeGoals = 5, AwayGoals = 0, IsPlayed = true},
                new Match(_teamB, _teamD, 1) { HomeGoals = 4, AwayGoals = 0, IsPlayed = true},
                new Match(_teamC, _teamD, 1) { HomeGoals = 1, AwayGoals = 0, IsPlayed = true},
            ]
        );
    }
    
    [TestMethod]
    public void AllTeamsShouldBeInRanking()
    {
        var ranking = RankingGenerator.GetRanking(_teams, _defaultGroup);
        
        Assert.AreEqual(1, ranking.Count(r => r.Team == _teamA.Name));
        Assert.AreEqual(1, ranking.Count(r => r.Team == _teamB.Name));
        Assert.AreEqual(1, ranking.Count(r => r.Team == _teamC.Name));
        Assert.AreEqual(1, ranking.Count(r => r.Team == _teamD.Name));
    }

    [TestMethod]
    public void NumberOfMatchesShouldBeCorrect()
    {
        var ranking = RankingGenerator.GetRanking(_teams, _defaultGroup);
        
        Assert.AreEqual(3, ranking.Single(r => r.Team == _teamA.Name).Played);
        Assert.AreEqual(3, ranking.Single(r => r.Team == _teamB.Name).Played);
        Assert.AreEqual(3, ranking.Single(r => r.Team == _teamC.Name).Played);
        Assert.AreEqual(3, ranking.Single(r => r.Team == _teamD.Name).Played);
    }

    [TestMethod]
    public void NumberOfWinsShouldBeCorrect()
    {
        var ranking = RankingGenerator.GetRanking(_teams, _defaultGroup);

        Assert.AreEqual(3, ranking.Single(r => r.Team == _teamA.Name).Win);
        Assert.AreEqual(2, ranking.Single(r => r.Team == _teamB.Name).Win);
        Assert.AreEqual(1, ranking.Single(r => r.Team == _teamC.Name).Win);
        Assert.AreEqual(0, ranking.Single(r => r.Team == _teamD.Name).Win);
    }

    [TestMethod]
    public void NumberOfDrawsShouldBeCorrect()
    {
        var group = new Group(
            [
                new Match(_teamA, _teamB, 1) { HomeGoals = 1, AwayGoals = 1, IsPlayed = true},
                new Match(_teamA, _teamC, 1) { HomeGoals = 1, AwayGoals = 1, IsPlayed = true},
                new Match(_teamA, _teamD, 1) { HomeGoals = 1, AwayGoals = 1, IsPlayed = true},
                new Match(_teamB, _teamC, 1) { HomeGoals = 1, AwayGoals = 1, IsPlayed = true},
                new Match(_teamB, _teamD, 1) { HomeGoals = 1, AwayGoals = 0, IsPlayed = true},
                new Match(_teamC, _teamD, 1) { HomeGoals = 1, AwayGoals = 2, IsPlayed = true},
            ]
        );
        var ranking = RankingGenerator.GetRanking(_teams, group);

        Assert.AreEqual(3, ranking.Single(r => r.Team == _teamA.Name).Draw);
        Assert.AreEqual(2, ranking.Single(r => r.Team == _teamB.Name).Draw);
        Assert.AreEqual(2, ranking.Single(r => r.Team == _teamC.Name).Draw);
        Assert.AreEqual(1, ranking.Single(r => r.Team == _teamD.Name).Draw);
    }

    [TestMethod]
    public void NumberOfLossesShouldBeCorrect()
    {
        var ranking = RankingGenerator.GetRanking(_teams, _defaultGroup);

        Assert.AreEqual(0, ranking.Single(r => r.Team == _teamA.Name).Loss);
        Assert.AreEqual(1, ranking.Single(r => r.Team == _teamB.Name).Loss);
        Assert.AreEqual(2, ranking.Single(r => r.Team == _teamC.Name).Loss);
        Assert.AreEqual(3, ranking.Single(r => r.Team == _teamD.Name).Loss);
    }

    [TestMethod]
    public void NumberOfGoalsShouldBeCorrect()
    {
        var ranking = RankingGenerator.GetRanking(_teams, _defaultGroup);

        Assert.AreEqual(6, ranking.Single(r => r.Team == _teamA.Name).For);
        Assert.AreEqual(0, ranking.Single(r => r.Team == _teamA.Name).Against);
        
        Assert.AreEqual(9, ranking.Single(r => r.Team == _teamB.Name).For);
        Assert.AreEqual(3, ranking.Single(r => r.Team == _teamB.Name).Against);
        
        Assert.AreEqual(1, ranking.Single(r => r.Team == _teamC.Name).For);
        Assert.AreEqual(7, ranking.Single(r => r.Team == _teamC.Name).Against);
        
        Assert.AreEqual(0, ranking.Single(r => r.Team == _teamD.Name).For);
        Assert.AreEqual(6, ranking.Single(r => r.Team == _teamD.Name).Against);
    }

    [TestMethod]
    public void NumberOfPointsShouldBeCorrect()
    {
        var group = new Group(
            [
                new Match(_teamA, _teamB, 1) { HomeGoals = 3, AwayGoals = 0, IsPlayed = true},
                new Match(_teamA, _teamC, 1) { HomeGoals = 2, AwayGoals = 0, IsPlayed = true},
                new Match(_teamA, _teamD, 1) { HomeGoals = 1, AwayGoals = 0, IsPlayed = true},
                new Match(_teamB, _teamC, 1) { HomeGoals = 2, AwayGoals = 1, IsPlayed = true},
                new Match(_teamB, _teamD, 1) { HomeGoals = 2, AwayGoals = 2, IsPlayed = true},
                new Match(_teamC, _teamD, 1) { HomeGoals = 1, AwayGoals = 0, IsPlayed = true},
            ]
        );

        var ranking = RankingGenerator.GetRanking(_teams, group);

        Assert.AreEqual(9, ranking.Single(r => r.Team == _teamA.Name).Points);
        Assert.AreEqual(4, ranking.Single(r => r.Team == _teamB.Name).Points);
        Assert.AreEqual(3, ranking.Single(r => r.Team == _teamC.Name).Points);
        Assert.AreEqual(1, ranking.Single(r => r.Team == _teamD.Name).Points);
    }
}