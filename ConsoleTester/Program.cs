using MiniSimulator.Data;
using MiniSimulator.Domain;

var sim = new Simulator();

var teamData = new StubRepository().GetTeams();

var teams = teamData
    .Select(t => new Team(t.Name, t.AttackScore / 100f,
        t.DefenseScore / 100f, t.Stamina / 100f, t.LateSurge))
    .ToArray();

var group = GroupGenerator.Generate(teams);
sim.Simulate(group);

foreach (var match in group.Matches)
{
    Console.WriteLine(match);
}

Console.WriteLine();

var ranking = RankingGenerator.GetRanking(teams, group);
Console.WriteLine("Pos.  Team       Played   Win    Draw   Loss   For   Against   Diff   Points");
foreach (var rankingItem in ranking)
{
    Console.WriteLine(rankingItem);
}