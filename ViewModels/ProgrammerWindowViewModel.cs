using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using extrakeys.Models;
using extrakeys.Services.Interfaces;
using ReactiveUI;

namespace extrakeys.ViewModels;

public class ProgrammerWindowViewModel : ViewModelBase
{
    private readonly IBoardProgrammerService _programmer;

    public ProgrammerWindowViewModel(IBoardProgrammerService programmer)
    {
        _programmer = programmer;
        LoadedKeybindings = new ObservableCollection<KeyBinding>();
        LoadedKeybindings.AddRange(_programmer.GetMacrosFromBoard());
        _programmer.BoardChanged += (sender, args) =>
        {
            this.RaisePropertyChanged(nameof(BoardInfoText));
            LoadedKeybindings.Clear();
            LoadedKeybindings.AddRange(_programmer.GetMacrosFromBoard());
            this.RaisePropertyChanged(nameof(LoadedKeybindings));
        };
    }

    public string BoardInfoText =>
        string.Format(Lang.Resources.ProgrammerWindow_board_info,
            _programmer.Board?.Port.PortName,
            _programmer.Board?.ButtonCols, _programmer.Board?.ButtonRows,
            _programmer.Board?.MacroCount);

    public ObservableCollection<KeyBinding> LoadedKeybindings { get; }

    public void AddKeyToKeyBinding(int buttonIndex, KeyCodeData key)
    {
        if (buttonIndex < 0 || buttonIndex > LoadedKeybindings.Count)
            return;
        if (_programmer.Board == null
            || LoadedKeybindings[buttonIndex].KeyCodes.Length >= _programmer.Board.MacroCount)
            return;

        var keybind = LoadedKeybindings[buttonIndex];

        LoadedKeybindings.RemoveAt(buttonIndex);
        LoadedKeybindings.Insert(buttonIndex, keybind.AddKey(key));
    }

    public void RemoveKeybinding(int buttonIndex, int keyCodeIndex)
    {
        if (buttonIndex < 0 || buttonIndex >= LoadedKeybindings.Count)
            return;
        if (LoadedKeybindings[buttonIndex].KeyCodes.Length <= keyCodeIndex)
            return;
        var keybind = LoadedKeybindings[buttonIndex];
        LoadedKeybindings.RemoveAt(buttonIndex);
        LoadedKeybindings.Insert(buttonIndex, keybind.RemoveKey(keyCodeIndex));
    }

    public void ResetKeybinds()
    {
        _programmer.ResetKeybindings();
    }
}