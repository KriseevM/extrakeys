using System.Collections.Generic;
using extrakeys.Models;

namespace extrakeys.Services.Interfaces;

public interface IBoardRepository
{
    public List<BoardData> BoardList { get; }
}