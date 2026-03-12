using MiniSimulator.Domain;

namespace MiniSimulator.Test;

[TestClass]
public class StaminaStrategyTest
{
    [TestMethod]
    [DataRow(1f, 1f, 1f, 90, 1f, 1f)]
    [DataRow(0.8f, 1f, 1f, 90, 0.8f, 0.8f)]
    [DataRow(0.8f, 1f, 1f, 45, 0.9f, 0.9f)]
    public void StrengthDecreasesLinearly(float stamina, float startAttack, float startDefense, int minute,
        float endAttack, float endDefense)
    {
        var sut = new StaminaStrategy();
        
        var home = new Team("home", 0, 0, stamina, false);
        var away = new Team("away", 0, 0, stamina, false);

        var matchState = new MatchState
        {
            TeamStates =
            [
                new TeamState { Team = home, EffectiveAttackStrength = startAttack, EffectiveDefenseStrength =  startDefense },
                new TeamState { Team = away, EffectiveAttackStrength = startAttack, EffectiveDefenseStrength =  startDefense  },
            ]
        };
        
        var result = sut.Calculate(matchState, minute);
        
        Assert.AreEqual(endAttack, result.TeamStates[0].EffectiveAttackStrength);
        Assert.AreEqual(endDefense, result.TeamStates[0].EffectiveDefenseStrength);
        Assert.AreEqual(endAttack, result.TeamStates[1].EffectiveAttackStrength);
        Assert.AreEqual(endDefense, result.TeamStates[1].EffectiveDefenseStrength);
    }
}