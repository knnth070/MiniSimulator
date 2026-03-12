using MiniSimulator.Data;
using MiniSimulator.Domain;

var sim = new Simulator();

var homeTeam = new Team("FC Home", 0.8f, .5f, 1, false);
var awayTeam = new Team("Visitors United", .8f, .3f, .8f, true);

// for (int i = 0; i < 10; i++)
{
    var teamData = new StubRepository().GetTeams();

    var teams = teamData
        .Select(t => new Team(t.Name, t.AttackScore / 100f, 
            t.DefenseScore / 100f, t.Stamina / 100f, t.LateSurge))
        .ToArray();
    var group = GroupGenerator.Generate(teams);

    Console.WriteLine(group.Matches.Count);
    foreach (var m in group.Matches)
    {
        Console.WriteLine(m);
    }
    
    // sim.Simulate(group);
    // Console.WriteLine(group.Matches.First());
}