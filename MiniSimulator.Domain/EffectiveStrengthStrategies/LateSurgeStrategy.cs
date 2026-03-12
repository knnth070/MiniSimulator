namespace MiniSimulator.Domain;

internal class LateSurgeStrategy : IEffectiveStrengthStrategy
{
    public MatchState Calculate(MatchState matchState, int minute)
    {
        if (minute < 75)
        {
            return matchState;
        }

        for (var i = 0; i < 2; i++)
        {
            if (matchState.TeamStates[i].Team.LateSurge &&
                matchState.TeamStates[i].Goals < matchState.TeamStates[1 - i].Goals)
            {
                matchState.TeamStates[i].EffectiveAttackStrength *= 1.25f;
                matchState.TeamStates[i].EffectiveDefenseStrength *= 0.75f;
            }
        }

        return matchState;
    }
}