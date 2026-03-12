using MiniSimulator.Domain;

namespace MiniSimulator.Test;

[TestClass]
public class LateSurgeStrategyTest
{
    [TestMethod]
    public void DoesNothingBeforeLastQuarter()
    {
        var sut = new LateSurgeStrategy();

        var home = new Team("home", 1, 1, 1, true);
        var away = new Team("away", 1, 1, 1, true);

        var matchState = new MatchState
        {
            TeamStates =
            [
                new TeamState { Team = home, EffectiveAttackStrength = 1, EffectiveDefenseStrength = 1, Goals = 0 },
                new TeamState { Team = away, Goals = 1 },
            ]
        };
        var result = sut.Calculate(matchState, 70);

        Assert.AreEqual(1, result.TeamStates[0].EffectiveAttackStrength);
        Assert.AreEqual(1, result.TeamStates[0].EffectiveDefenseStrength);
    }

    [TestMethod]
    [DataRow(1f, 1f, 1.25f, 0.75f)]
    [DataRow(0.8f, 0.7f, 1f, 0.525f)]
    public void IsAppliedInLastQuarter(float attack, float defense, float attackResult, float defenseResult)
    {
        var sut = new LateSurgeStrategy();

        var home = new Team("home", 1, 1, 1, true);
        var away = new Team("away", 1, 1, 1, true);

        var matchState = new MatchState
        {
            TeamStates =
            [
                new TeamState { Team = home, EffectiveAttackStrength = attack, EffectiveDefenseStrength = defense, Goals = 0 },
                new TeamState { Team = away, Goals = 1 },
            ]
        };
        var result = sut.Calculate(matchState, 80);

        Assert.AreEqual(attackResult, result.TeamStates[0].EffectiveAttackStrength);
        Assert.AreEqual(defenseResult, result.TeamStates[0].EffectiveDefenseStrength);
    }

    [TestMethod]
    public void IsNotAppliedToLeadingTeam()
    {
        var sut = new LateSurgeStrategy();

        var home = new Team("home", 1, 1, 1, true);
        var away = new Team("away", 1, 1, 1, true);

        var matchState = new MatchState
        {
            TeamStates =
            [
                new TeamState { Team = home, EffectiveAttackStrength = 1, EffectiveDefenseStrength = 1, Goals = 1 },
                new TeamState { Team = away, Goals = 0 },
            ]
        };
        var result = sut.Calculate(matchState, 80);

        Assert.AreEqual(1, result.TeamStates[0].EffectiveAttackStrength);
        Assert.AreEqual(1, result.TeamStates[0].EffectiveDefenseStrength);
    }
}