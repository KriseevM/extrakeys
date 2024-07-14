using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using extrakeys.Models;
using ReactiveUI;

namespace extrakeys.ViewModels;

public class NewKeybindWindowViewModel : ViewModelBase
{
    private string _filterText = "";
    private int _selectedFilteredIndex = -1;

    public int SelectedFilteredIndex
    {
        get => _selectedFilteredIndex;
        set
        {
            _selectedFilteredIndex = value;
            if (value != -1)
            {
                this.SelectedKeyCode = Models.KeyCodeData.PreDefinedKeyCodes.First(p => p.DisplayName == FilteredKeyNames[value]);
            }
        }
    }

    public KeyCodeData? SelectedKeyCode { get; set; }
    public bool Success { get; set; } = false;

    public List<string> KeyNames => Models.KeyCodeData.PreDefinedKeyCodes.ConvertAll(p => p.DisplayName);
    public List<string> FilteredKeyNames => KeyNames.Where(p => p.Contains(_filterText, System.StringComparison.InvariantCultureIgnoreCase)).ToList();

    public string FilterText
    {
        get => _filterText;
        set
        {
            _filterText = value;
            this.RaisePropertyChanged();
            this.RaisePropertyChanged(nameof(FilteredKeyNames));
            if (SelectedKeyCode == null || FilteredKeyNames.All(p => p != SelectedKeyCode.DisplayName)) return;
            SelectedFilteredIndex = FilteredKeyNames.IndexOf(SelectedKeyCode.DisplayName);
            this.RaisePropertyChanged(nameof(SelectedFilteredIndex));
        }
    }
}