namespace MiniSimulator.Domain;

public class Team(string name, float attackScore, float defenseScore, float stamina, bool lateSurge)
{
    /// <summary>
    /// The name of the team
    /// </summary>
    public string Name { get; } = name;
    /// <summary>
    /// The strength of the team's attack, rated 0-1
    /// </summary>
    public float AttackScore { get; } = attackScore;
    /// <summary>
    /// The strength of the team's defense, rated 0-1
    /// </summary>
    public float DefenseScore { get; } = defenseScore;
    /// <summary>
    /// Stamina of the team. During a match, the attack and
    /// defense scores will linearly degrade.
    /// E.g. with a stamina of 0.8 the effective attack score
    /// in the last minute will be 80% of its original value
    /// </summary>
    public float Stamina { get; } = stamina;
    /// <summary>
    /// If the team has a late surge, their attack score, when behind in
    /// the last 15 minutes, will increase with 25% but their defense score
    /// will decrease with 25%.
    /// </summary>
    public bool LateSurge { get; } = lateSurge;
}