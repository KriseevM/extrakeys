using System;
using System.Collections.Generic;
using System.Linq;
using DynamicData;
using extrakeys.Models;
using extrakeys.Services.Interfaces;

namespace extrakeys.Services;

public class BoardProgrammingServiceImpl : IBoardProgrammerService
{
    private readonly Object _boardLock = new();
    private BoardData? _board;

    public BoardData? Board
    {
        get => _board;
        set
        {
            lock (_boardLock)
            {
                _board = value;
            }
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

    public void ProgramMacro(KeyBinding macro)
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
                    $"This board only supports up to {_board.MacroCount} keys per button. " +
                    $"{macro.KeyCodes.Length} is too many");
            }

            if (macro.KeyNumber >= _board.ButtonCols * _board.ButtonRows)
            {
                throw new ArgumentOutOfRangeException(nameof(macro.KeyNumber), macro.KeyNumber,
                    $"This board only supports {_board.ButtonCols * _board.ButtonRows} buttons");
            }
            try
            {
                _board.Port.Open();
                List<byte> programmingData = new List<byte>();
                programmingData.Add(0xAA);
                programmingData.Add((byte) macro.KeyNumber);
                for (int i = 0; i < _board.MacroCount; ++i)
                {
                    if (i < macro.KeyCodes.Length)
                    {
                        programmingData.Add(macro.KeyCodes[i].KeyCode);
                    }
                    else
                    {
                        programmingData.Add(0);
                    }
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
                for (int i = 0; i < _board.ButtonCols * _board.ButtonRows; ++i)
                {
                    List<KeyCodeData> macro = new List<KeyCodeData>();
                    for (int j = 0; j < _board.MacroCount; ++j)
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