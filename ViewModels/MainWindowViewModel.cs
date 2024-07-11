using System.Collections.Generic;
using extrakeys.Services.Interfaces;

namespace extrakeys.ViewModels;

public class MainWindowViewModel(IBoardRepository repository, IBoardProgrammerService programmer)
    : ViewModelBase
{
    private readonly IBoardRepository _repository = repository;
    private readonly IBoardProgrammerService _programmer = programmer;
    
    
}
