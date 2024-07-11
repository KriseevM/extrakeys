using System.Collections.Generic;
using System.IO.Ports;
using extrakeys.Models;

namespace extrakeys.Services.Interfaces;

public interface IBoardProgrammerService
{
    public BoardData? Board { get; set; }

    public void ResetKeybindings();

    public void ProgramMacro(KeyBinding macro);

    public List<KeyBinding> GetMacrosFromBoard();
}