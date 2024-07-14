using System.Collections.Generic;

namespace extrakeys.ViewModels;

public class NewKeybindWindowViewModel : ViewModelBase
{
    public int SelectedIndex { get; set; } = -1;
    public bool Success { get; set; } = false;

    public List<string> KeyNames => Models.KeyCodeData.PreDefinedKeyCodes.ConvertAll(p => p.DisplayName);
}