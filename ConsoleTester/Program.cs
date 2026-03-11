// See https://aka.ms/new-console-template for more information

using MiniSimulator.Domain;

var sim = new Simulator();

var homeTeam = new Team("FC Home", 0.8f, .4f, 1, false);
var awayTeam = new Team("Visitors United", .6f, .0f, .8f, false);

for (int i = 0; i < 10; i++)
{
    var group = new Group([new Match(homeTeam, awayTeam)]);

    sim.Simulate(group);

    Console.WriteLine(group.Matches.First());
}