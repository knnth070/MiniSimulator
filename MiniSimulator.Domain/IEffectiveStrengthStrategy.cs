namespace MiniSimulator.Domain;

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