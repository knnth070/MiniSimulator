namespace MiniSimulator.Domain;

public class Simulator
{
    private const float AttemptsPerMatch = 6;

    private readonly IEffectiveStrengthStrategy _effectiveStrengthStrategy =
        new CompositeStrategy(new InitialStrengthStrategy(), new StaminaStrategy(), new LateSurgeStrategy());

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