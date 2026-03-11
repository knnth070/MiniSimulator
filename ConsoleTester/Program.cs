using MiniSimulator.Domain;

var sim = new Simulator();

var homeTeam = new Team("FC Home", 0.8f, .5f, 1, false);
var awayTeam = new Team("Visitors United", .8f, .3f, .8f, true);

for (int i = 0; i < 10; i++)
{
    var group = new Group([new Match(homeTeam, awayTeam)]);

    sim.Simulate(group);

    Console.WriteLine(group.Matches.First());
}