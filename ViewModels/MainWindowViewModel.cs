using System.Collections.Generic;
using System.Linq;
using extrakeys.Services.Interfaces;
using ReactiveUI;

namespace extrakeys.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IBoardRepository _repository;
    private readonly IBoardProgrammerService _programmer;
    private int _selectedComPort = -1;
    private bool _startAvailable;

    public MainWindowViewModel(IBoardRepository repository, IBoardProgrammerService programmer)
    {
        _repository = repository;
        _programmer = programmer;
        _repository.Boards.CollectionChanged += (_, _) => this.RaisePropertyChanged(nameof(BoardNames));
    }

    public List<string> BoardNames => _repository.Boards.ToList().ConvertAll(p => p.ToString());

    public int SelectedComPort
    {
        get => _selectedComPort;
        set
        {
            _selectedComPort = value;
            _programmer.Board = _repository.Boards[_selectedComPort];
            StartAvailable = (value != -1);
        }
    }

    public void RefreshBoardList()
    {
        _repository.Refresh();
        this.RaisePropertyChanged(nameof(BoardNames));
    }

    public bool StartAvailable
    {
        get => _startAvailable;
        private set
        {
            _startAvailable = value;
            this.RaisePropertyChanged();
        }
    }
}