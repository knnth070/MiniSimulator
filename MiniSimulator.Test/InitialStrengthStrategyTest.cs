using MiniSimulator.Domain;

namespace MiniSimulator.Test;

[TestClass]
public class InitialStrengthStrategyTest
{
    [TestMethod]
    [DataRow(1f, 1f, 0.8f, 0.8f)]
    [DataRow(0.8f, 0.4f, 0.75f, 0.95f)]
    [DataRow(0.64f, 0.85f, 0.79f, 0.35f)]
    public void CopiesFromTeam(float homeAttack, float homeDefense, float awayAttack, float awayDefense)
    {
        var sut = new InitialStrengthStrategy();
        
        var home = new Team("home", homeAttack, homeDefense, 1, true);
        var away = new Team("away", awayAttack, awayDefense, 1, true);

        var matchState = new MatchState
        {
            TeamStates =
            [
                new TeamState { Team = home },
                new TeamState { Team = away },
            ]
        };
        
        var result = sut.Calculate(matchState, 1);
        
        Assert.AreEqual(homeAttack, result.TeamStates[0].EffectiveAttackStrength);
        Assert.AreEqual(homeDefense, result.TeamStates[0].EffectiveDefenseStrength);
        Assert.AreEqual(awayAttack, result.TeamStates[1].EffectiveAttackStrength);
        Assert.AreEqual(awayDefense, result.TeamStates[1].EffectiveDefenseStrength);
    }
}