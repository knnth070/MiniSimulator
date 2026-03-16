using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using MiniSimulator.Data;
using MiniSimulator.Domain;

namespace MiniSimulator.UI;

public class MainWindowViewModel : ViewModelBase
{
    private ObservableCollection<MatchViewModel> _matches = [];
    private ObservableCollection<RankingViewModel> _ranking = [];
    public ICommand RerunCommand { get; }

    public MainWindowViewModel()
    {
        RerunCommand = new DefaultCommand(RunSimulator);
        RunSimulator();
    }

    public ObservableCollection<MatchViewModel> Matches
    {
        get => _matches;
        set
        {
            _matches = value;
            OnPropertyChanged(nameof(Matches));
        }
    }

    public ObservableCollection<RankingViewModel> Ranking
    {
        get => _ranking;
        set
        {
            _ranking = value;
            OnPropertyChanged(nameof(Ranking));
        }
    }

    private void RunSimulator()
    {
        var teamData = new StubRepository().GetTeams();

        var teams = teamData
            .Select(t => new Team(t.Name, t.AttackScore / 100f,
                t.DefenseScore / 100f, t.Stamina / 100f, t.LateSurge))
            .ToArray();

        var group = GroupGenerator.Generate(teams);
        Simulator.Simulate(group);
        var ranking = RankingGenerator.GetRanking(teams, group);

        Matches = new ObservableCollection<MatchViewModel>(
            group.Matches
                .Select(m => new MatchViewModel
                {
                    HomeTeam = m.HomeTeam.Name,
                    AwayTeam = m.AwayTeam.Name,
                    Round = m.Round,
                    Result = m.IsPlayed ? $"{m.HomeGoals} -  {m.AwayGoals}" : ""
                })
        );
        Ranking = new ObservableCollection<RankingViewModel>(
            ranking
                .Select(r => new RankingViewModel
                {
                    Position = r.Position,
                    Team = r.Team,
                    Played = r.Played,
                    Win = r.Win,
                    Draw = r.Draw,
                    Loss = r.Loss,
                    For = r.For,
                    Against = r.Against,
                    Diff = r.For - r.Against,
                    Points = r.Points
                }));
    }
}