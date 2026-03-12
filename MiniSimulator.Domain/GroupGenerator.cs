namespace MiniSimulator.Domain;

// uses https://en.wikipedia.org/wiki/Round-robin_tournament#Circle_method

public static class GroupGenerator
{
    public static Group Generate(Team[] teams)
    {
        var matches = new List<Match>();

        var tempTeams = teams.ToList();

        for (var round = 1; round <= teams.Length - 1; round++)
        {
            for (var i = 0; i < teams.Length / 2; i++)
            {
                var home = tempTeams[i];
                var away = tempTeams[teams.Length - 1 - i];

                // Make sure first team doesn't always play at home
                if (i == 0 && round > teams.Length / 2)
                {
                    (home, away) = (away, home);
                }
                var match = new Match(home, away, round);
                matches.Add(match);
            }

            var last = tempTeams.Last();
            tempTeams.Remove(last);
            tempTeams.Insert(1, last);
        }

        return new Group(matches);
    }
}