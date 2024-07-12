using System.Collections.Generic;
using System.Collections.ObjectModel;
using extrakeys.Models;

namespace extrakeys.Services.Interfaces;

public interface IBoardRepository
{
    public ObservableCollection<BoardData> Boards { get; }
    public void Refresh();
}