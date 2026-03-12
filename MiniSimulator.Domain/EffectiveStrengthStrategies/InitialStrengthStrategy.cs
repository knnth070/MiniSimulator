namespace MiniSimulator.Domain;

public class InitialStrengthStrategy : IEffectiveStrengthStrategy
{
    // Only copy attack/defense to TeamState so subsequent strategies can work from that
    public MatchState Calculate(MatchState matchState, int minute)
    {
        var home = matchState.TeamStates[0];
        var away = matchState.TeamStates[1];
        return new MatchState
        {
            TeamStates =
            [
                home with
                {
                    EffectiveAttackStrength = home.Team.AttackScore,
                    EffectiveDefenseStrength = home.Team.DefenseScore
                },
                away with
                {
                    EffectiveAttackStrength = away.Team.AttackScore,
                    EffectiveDefenseStrength = away.Team.DefenseScore
                },
            ]
        };
    }
}