using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using extrakeys.Models;
using extrakeys.Services.Interfaces;
using ReactiveUI;

namespace extrakeys.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IBoardRepository _repository;
    private readonly IBoardProgrammerService _programmer;

    public MainWindowViewModel(IBoardRepository repository, IBoardProgrammerService programmer)
    {
        _repository = repository;
        _programmer = programmer;
        _repository.Boards.CollectionChanged += (sender, args) => this.RaisePropertyChanged(nameof(BoardNames));
    }

    public List<string> BoardNames => _repository.Boards.ToList().ConvertAll(p => p.ToString());
    
    public int SelectedComPort { get; set; } = -1;

    public void RefreshBoardList()
    {
        _repository.Refresh();
        this.RaisePropertyChanged(nameof(BoardNames));
    }
}