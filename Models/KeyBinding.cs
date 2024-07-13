using System;
using System.Collections.Generic;

namespace extrakeys.Models;

public record KeyBinding
{
    public required int KeyNumber { get; init; }
    public required KeyCodeData[] KeyCodes { get; init; }

    public KeyBinding AddKey(KeyCodeData key)
    {
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
}