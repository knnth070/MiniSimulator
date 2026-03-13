namespace MiniSimulator.Domain;

public struct TeamState
{
    public Team Team { get; init; }
    public float EffectiveAttackStrength { get; set; }
    public float EffectiveDefenseStrength { get; set; }
    public int Goals { get; set; }
}