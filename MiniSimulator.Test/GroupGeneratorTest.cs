using MiniSimulator.Domain;

namespace MiniSimulator.Test;

[TestClass]
public class GroupGeneratorTest
{
    private readonly Team[] _teams =
    [
        new Team("A", 1, 1, 1, false),
        new Team("B", 1, 1, 1, false),
        new Team("C", 1, 1, 1, false),
        new Team("D", 1, 1, 1, false)
    ];

    [TestMethod]
    public void TeamsPlayHomeAndAway()
    {
        var group = GroupGenerator.Generate(_teams);

        Assert.IsTrue(group.Matches.Exists(m => m.HomeTeam.Name == "A"));
        Assert.IsTrue(group.Matches.Exists(m => m.AwayTeam.Name == "A"));
        Assert.IsTrue(group.Matches.Exists(m => m.HomeTeam.Name == "B"));
        Assert.IsTrue(group.Matches.Exists(m => m.AwayTeam.Name == "B"));
        Assert.IsTrue(group.Matches.Exists(m => m.HomeTeam.Name == "C"));
        Assert.IsTrue(group.Matches.Exists(m => m.AwayTeam.Name == "C"));
        Assert.IsTrue(group.Matches.Exists(m => m.HomeTeam.Name == "D"));
        Assert.IsTrue(group.Matches.Exists(m => m.AwayTeam.Name == "D"));
    }

    [TestMethod]
    public void NoTeamPlaysItself()
    {
        var group = GroupGenerator.Generate(_teams);
        Assert.IsFalse(group.Matches.Any(m => m.HomeTeam.Name == m.AwayTeam.Name));
    }

    [TestMethod]
    public void TeamsPlayEveryoneExactlyOnce()
    {
        var group = GroupGenerator.Generate(_teams);

        var result = group.Matches
            .Select(m =>
                string.Compare(m.HomeTeam.Name, m.AwayTeam.Name, StringComparison.InvariantCulture) < 0
                    ? $"{m.HomeTeam.Name}{m.AwayTeam.Name}"
                    : $"{m.AwayTeam.Name}{m.HomeTeam.Name}")
            .ToArray();

        Assert.AreEqual(6, result.Length);
        Assert.IsTrue(result.Contains("AB"));
        Assert.IsTrue(result.Contains("AC"));
        Assert.IsTrue(result.Contains("AD"));
        Assert.IsTrue(result.Contains("BC"));
        Assert.IsTrue(result.Contains("BD"));
        Assert.IsTrue(result.Contains("CD"));
    }

    [TestMethod]
    [DataRow(4, 3)]
    [DataRow(8, 7)]
    [DataRow(10, 9)]
    public void RoundsMatchGroupSize(int numberOfTeams, int expectedRounds)
    {
        var teams = Enumerable.Range(1, numberOfTeams)
            .Select(i => new Team($"Team {i}", 1, 1, 1, false))
            .ToArray();

        var group = GroupGenerator.Generate(teams);

        var rounds = group.Matches
            .Select(m => m.Round)
            .OrderBy(x => x)
            .Distinct()
            .Count();

        Assert.AreEqual(expectedRounds, rounds);
    }
}