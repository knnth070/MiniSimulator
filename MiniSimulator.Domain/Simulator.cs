namespace MiniSimulator.Domain;

public class Simulator
{
    private const float AttemptsPerMatch = 6;

    private readonly IEffectiveStrengthStrategy _effectiveStrengthStrategy =
        new CompositeStrategy(new StaminaStrategy(), new LateSurgeStrategy());

    public void Simulate(Group group)
    {
        foreach (var match in group.Matches.Where(m => !m.IsPlayed))
        {
            SimulateMatch(match);
        }
    }

    private void SimulateMatch(Match match)
    {
        Random random = new();

        var matchState = new MatchState
        {
            TeamStates =
            [
                new TeamState { Team = match.HomeTeam },
                new TeamState { Team = match.AwayTeam }
            ]
        };

        for (var minute = 1; minute <= 90; minute++)
        {
            if (!(random.NextSingle() < AttemptsPerMatch / 90f)) continue;

            matchState = _effectiveStrengthStrategy.Calculate(matchState, minute);
            
            // Possession is based on effective attack strength relative to the opposition
            var homeEffectiveAttackStrength = matchState.TeamStates[0].EffectiveAttackStrength;
            var awayEffectiveAttackStrength = matchState.TeamStates[1].EffectiveAttackStrength;

            var possession = homeEffectiveAttackStrength / (homeEffectiveAttackStrength + awayEffectiveAttackStrength) >
                             random.NextSingle()
                ? 0
                : 1;

            if (matchState.TeamStates[1 - possession].EffectiveDefenseStrength < random.NextSingle())
                matchState.TeamStates[possession].Goals++;
        }

        match.HomeGoals = matchState.TeamStates[0].Goals;
        match.AwayGoals = matchState.TeamStates[1].Goals;
        match.IsPlayed = true;
    }
}

public struct TeamState
{
    public Team Team { get; init; }
    public float EffectiveAttackStrength { get; set; }
    public float EffectiveDefenseStrength { get; set; }
    public int Goals { get; set; }
}

public struct MatchState
{
    public TeamState[] TeamStates = new TeamState[2];

    public MatchState()
    {
    }
}

public interface IEffectiveStrengthStrategy
{
    /// <summary>
    /// Calculates the effective attack and defense strength of a team at a given point in the match
    /// </summary>
    /// <param name="matchState">The state of the ongoing match</param>
    /// <param name="minute">The point in time for which to calculate</param>
    /// <returns>The updated match state</returns>
    MatchState Calculate(MatchState matchState, int minute);
}

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

internal class CompositeStrategy(params IEffectiveStrengthStrategy[] strategies) : IEffectiveStrengthStrategy
{
    public MatchState Calculate(MatchState matchState, int minute) =>
        strategies.Aggregate(matchState,
            (current, strategy) => strategy.Calculate(current, minute));
}