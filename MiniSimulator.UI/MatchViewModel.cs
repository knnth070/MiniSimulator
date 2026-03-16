namespace MiniSimulator.UI;

public class MatchViewModel : ViewModelBase
{
    private string _homeTeam = string.Empty;
    private string _awayTeam = string.Empty;
    private int _round;
    private string _result = string.Empty;

    public string HomeTeam
    {
        get => _homeTeam;
        set
        {
            _homeTeam = value;
            OnPropertyChanged(nameof(HomeTeam));
        }
    }

    public string AwayTeam
    {
        get => _awayTeam;
        set
        {
            _awayTeam = value;
            OnPropertyChanged(nameof(AwayTeam));
        }
    }

    public int Round
    {
        get => _round;
        set
        {
            _round = value;
            OnPropertyChanged(nameof(Round));
        }
    }

    public string Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged(nameof(Result));
        }
    }
}