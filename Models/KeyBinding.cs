using System.Collections.Generic;
using System.Linq;

namespace extrakeys.Models;

public record KeyBinding
{
    public required int KeyNumber { get; init; }
    public required KeyCodeData[] KeyCodes { get; init; }
    
    public bool IsNotEmpty => KeyCodes.Length > 0;

    public KeyBinding AddKey(KeyCodeData key)
    {
        if (KeyCodes.Any(x => x == key))
        {
            return this;
        }
        List<KeyCodeData> newKeyCodeList = [];
        newKeyCodeList.AddRange(KeyCodes);
        newKeyCodeList.Add(key);
        return this with { KeyCodes = newKeyCodeList.ToArray() };
    }

    public KeyBinding RemoveKey(int index)
    {
        List<KeyCodeData> newKeyCodeList = [];
        newKeyCodeList.AddRange(KeyCodes);
        newKeyCodeList.RemoveAt(index);
        return this with { KeyCodes = newKeyCodeList.ToArray() };
    }

    public KeyBinding Clean()
    {
        return this with { KeyCodes = [] };
    }
}