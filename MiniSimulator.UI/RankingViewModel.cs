namespace MiniSimulator.UI;

public class RankingViewModel : ViewModelBase
{
    private int _position;
    private string _team = string.Empty;
    private int _played;
    private int _win;
    private int _draw;
    private int _loss;
    private int _for;
    private int _against;
    private int _diff;
    private int _points;

    public int Position
    {
        get => _position;
        set
        {
            _position = value;
            OnPropertyChanged(nameof(Position));
        }
    }
    public string Team
    {
        get => _team;
        set
        {
            _team = value;
            OnPropertyChanged(nameof(Team));
        }
    }

    public int Played
    {
        get => _played;
        set
        {
            _played = value;
            OnPropertyChanged(nameof(Played));
        }
    }

    public int Win
    {
        get => _win;
        set
        {
            _win = value;
            OnPropertyChanged(nameof(Win));
        }
    }

    public int Draw
    {
        get => _draw;
        set
        {
            _draw = value;
            OnPropertyChanged(nameof(Draw));
        }
    }

    public int Loss
    {
        get => _loss;
        set
        {
            _loss = value;
            OnPropertyChanged(nameof(Loss));
        }
    }

    public int For
    {
        get => _for;
        set
        {
            _for = value;
            OnPropertyChanged(nameof(For));
        }
    }

    public int Against
    {
        get => _against;
        set
        {
            _against = value;
            OnPropertyChanged(nameof(Against));
        }
    }

    public int Diff
    {
        get => _diff;
        set
        {
            _diff = value;
            OnPropertyChanged(nameof(Diff));
        }
    }

    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            OnPropertyChanged(nameof(Points));
        }
    }
}