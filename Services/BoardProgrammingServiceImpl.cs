using System;
using System.Collections.Generic;
using System.Linq;
using extrakeys.Lang;
using extrakeys.Models;
using extrakeys.Services.Interfaces;

namespace extrakeys.Services;

public class BoardProgrammingServiceImpl : IBoardProgrammerService
{
    private readonly object _boardLock = new();
    private  BoardData? _board;

    public event EventHandler? BoardChanged;
    
    public BoardData? Board
    {
        get => _board;
        set
        {
            lock (_boardLock)
            {
                _board = value;
            }
            BoardChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetKeybindings()
    {
        lock (_boardLock)
        {
            if (_board == null)
            {
                throw new InvalidOperationException("Board must be initialized before this call");
            }

            try
            {
                _board.Port.Open();
                _board.Port.Write(new byte[1] { 0xF0 }, 0, 1);
            }
            finally
            {
                _board.Port.Close();
            }
        }
    }

    public void UploadMacro(KeyBinding macro)
    {
        lock (_boardLock)
        {
            if (_board == null)
            {
                throw new InvalidOperationException("Board must be initialized before this call");
            }

            if (_board.MacroCount < macro.KeyCodes.Length)
            {
                throw new InvalidOperationException(
                    $"This board only supports up to {_board.MacroCount} keys per button. {macro.KeyCodes.Length} is too many");
            }

            if (macro.KeyNumber >= _board.ButtonCols * _board.ButtonRows)
            {
                throw new ArgumentOutOfRangeException(nameof(macro.KeyNumber), macro.KeyNumber,
                    string.Format(Resources.BoardProgrammingService_button_out_of_range, _board.ButtonCols * _board.ButtonRows));
            }

            try
            {
                _board.Port.Open();
                List<byte> programmingData =
                [
                    0xAA,
                    (byte)macro.KeyNumber
                ];
                for (var i = 0; i < _board.MacroCount; ++i)
                {
                    programmingData.Add(i < macro.KeyCodes.Length ? macro.KeyCodes[i].KeyCode : (byte)0);
                }

                _board.Port.Write(programmingData.ToArray(), 0, programmingData.Count);
            }
            finally
            {
                _board.Port.Close();
            }
        }
    }

    public List<KeyBinding> GetMacrosFromBoard()
    {
        lock (_boardLock)
        {
            if (_board == null)
            {
                throw new InvalidOperationException("Board must be initialized before this call");
            }

            try
            {
                _board.Port.Open();
                _board.Port.Write(new byte[1] { 0xAE }, 0, 1);
                var keybinds = new List<KeyBinding>();
                for (var i = 0; i < _board.ButtonCols * _board.ButtonRows; ++i)
                {
                    List<KeyCodeData> macro = [];
                    for (var j = 0; j < _board.MacroCount; ++j)
                    {
                        var val = _board.Port.ReadByte();
                        if (val == 0)
                        {
                            break;
                        }

                        macro.Add(KeyCodeData.PreDefinedKeyCodes.FirstOrDefault(p => p.KeyCode == val,
                            new KeyCodeData() { DisplayName = $"{val}", KeyCode = (byte)val }));
                    }

                    keybinds.Add(new KeyBinding()
                    {
                        KeyNumber = i,
                        KeyCodes = macro.ToArray(),
                    });
                }

                return keybinds;
            }
            finally
            {
                _board.Port.Close();
            }
        }
    }

}