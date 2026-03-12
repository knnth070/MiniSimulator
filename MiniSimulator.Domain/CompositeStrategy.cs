namespace MiniSimulator.Domain;

internal class CompositeStrategy(params IEffectiveStrengthStrategy[] strategies) : IEffectiveStrengthStrategy
{
    public MatchState Calculate(MatchState matchState, int minute) =>
        strategies.Aggregate(matchState,
            (current, strategy) => strategy.Calculate(current, minute));
}