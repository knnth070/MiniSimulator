namespace MiniSimulator.Domain;

internal class StaminaStrategy : IEffectiveStrengthStrategy
{
    public MatchState Calculate(MatchState matchState, int minute)
    {
        var teamStates = matchState.TeamStates;

        for (var i = 0; i < 2; i++)
        {
            var (attack, defense) = CalculateEffectiveStrength(matchState.TeamStates[i].Team, minute);
            teamStates[i].EffectiveAttackStrength = attack;
            teamStates[i].EffectiveDefenseStrength = defense;
        }

        return new MatchState { TeamStates = teamStates };
    }

    private (float, float) CalculateEffectiveStrength(Team team, int minute)
    {
        var effectiveStamina = 1 + ((team.Stamina - 1) / 90) * minute;
        return (team.AttackScore * effectiveStamina, team.DefenseScore * effectiveStamina);
    }
}