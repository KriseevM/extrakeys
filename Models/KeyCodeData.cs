using System.Collections.Generic;

namespace extrakeys.Models;

public record KeyCodeData
{
    public required byte KeyCode { get; init; }
    public required string DisplayName { get; init; }

    public static readonly List<KeyCodeData> PreDefinedKeyCodes =
    [
        // printable ASCII
        new KeyCodeData() { KeyCode = 0x61, DisplayName = "A" },
        new KeyCodeData() { KeyCode = 0x62, DisplayName = "B" },
        new KeyCodeData() { KeyCode = 0x63, DisplayName = "C" },
        new KeyCodeData() { KeyCode = 0x64, DisplayName = "D" },
        new KeyCodeData() { KeyCode = 0x65, DisplayName = "E" },
        new KeyCodeData() { KeyCode = 0x66, DisplayName = "F" },
        new KeyCodeData() { KeyCode = 0x67, DisplayName = "G" },
        new KeyCodeData() { KeyCode = 0x68, DisplayName = "H" },
        new KeyCodeData() { KeyCode = 0x69, DisplayName = "I" },
        new KeyCodeData() { KeyCode = 0x6a, DisplayName = "J" },
        new KeyCodeData() { KeyCode = 0x6b, DisplayName = "K" },
        new KeyCodeData() { KeyCode = 0x6c, DisplayName = "L" },
        new KeyCodeData() { KeyCode = 0x6d, DisplayName = "M" },
        new KeyCodeData() { KeyCode = 0x6e, DisplayName = "N" },
        new KeyCodeData() { KeyCode = 0x6f, DisplayName = "O" },
        new KeyCodeData() { KeyCode = 0x70, DisplayName = "P" },
        new KeyCodeData() { KeyCode = 0x71, DisplayName = "Q" },
        new KeyCodeData() { KeyCode = 0x72, DisplayName = "R" },
        new KeyCodeData() { KeyCode = 0x73, DisplayName = "S" },
        new KeyCodeData() { KeyCode = 0x74, DisplayName = "T" },
        new KeyCodeData() { KeyCode = 0x75, DisplayName = "U" },
        new KeyCodeData() { KeyCode = 0x76, DisplayName = "V" },
        new KeyCodeData() { KeyCode = 0x77, DisplayName = "W" },
        new KeyCodeData() { KeyCode = 0x78, DisplayName = "X" },
        new KeyCodeData() { KeyCode = 0x79, DisplayName = "Y" },
        new KeyCodeData() { KeyCode = 0x7a, DisplayName = "Z" },
        new KeyCodeData() { KeyCode = 0x7b, DisplayName = "{" },
        new KeyCodeData() { KeyCode = 0x7c, DisplayName = "|" },
        new KeyCodeData() { KeyCode = 0x7d, DisplayName = "}" },
        new KeyCodeData() { KeyCode = 0x7e, DisplayName = "~" },
        new KeyCodeData() { KeyCode = 0x30, DisplayName = "0" },
        new KeyCodeData() { KeyCode = 0x31, DisplayName = "1" },
        new KeyCodeData() { KeyCode = 0x32, DisplayName = "2" },
        new KeyCodeData() { KeyCode = 0x33, DisplayName = "3" },
        new KeyCodeData() { KeyCode = 0x34, DisplayName = "4" },
        new KeyCodeData() { KeyCode = 0x35, DisplayName = "5" },
        new KeyCodeData() { KeyCode = 0x36, DisplayName = "6" },
        new KeyCodeData() { KeyCode = 0x37, DisplayName = "7" },
        new KeyCodeData() { KeyCode = 0x38, DisplayName = "8" },
        new KeyCodeData() { KeyCode = 0x39, DisplayName = "9" },
        new KeyCodeData() { KeyCode = 0x3a, DisplayName = ":" },
        new KeyCodeData() { KeyCode = 0x3b, DisplayName = ";" },
        new KeyCodeData() { KeyCode = 0x3c, DisplayName = "<" },
        new KeyCodeData() { KeyCode = 0x3d, DisplayName = "=" },
        new KeyCodeData() { KeyCode = 0x3e, DisplayName = ">" },
        new KeyCodeData() { KeyCode = 0x3f, DisplayName = "?" },
        new KeyCodeData() { KeyCode = 0x40, DisplayName = "@" },
        new KeyCodeData() { KeyCode = 0x20, DisplayName = "Space" },
        new KeyCodeData() {KeyCode = 0x21, DisplayName = "!" },
        new KeyCodeData() {KeyCode = 0x22, DisplayName = "\"" },
        new KeyCodeData() {KeyCode = 0x23, DisplayName = "#" },
        new KeyCodeData() {KeyCode = 0x24, DisplayName = "$" },
        new KeyCodeData() {KeyCode = 0x25, DisplayName = "%" },
        new KeyCodeData() {KeyCode = 0x26, DisplayName = "&" },
        new KeyCodeData() {KeyCode = 0x27, DisplayName = "'" },
        new KeyCodeData() {KeyCode = 0x28, DisplayName = "(" },
        new KeyCodeData() {KeyCode = 0x29, DisplayName = ")" },
        new KeyCodeData() {KeyCode = 0x2a, DisplayName = "*" },
        new KeyCodeData() {KeyCode = 0x2b, DisplayName = "+" },
        new KeyCodeData() {KeyCode = 0x2c, DisplayName = "," },
        new KeyCodeData() {KeyCode = 0x2d, DisplayName = "-" },
        new KeyCodeData() {KeyCode = 0x2e, DisplayName = "." },
        new KeyCodeData() {KeyCode = 0x2f, DisplayName = "/" },
        new KeyCodeData() {KeyCode = 0x60, DisplayName = "`" },
        // modifiers
        new KeyCodeData() {KeyCode = 0x80, DisplayName = "Left Ctrl" },
        new KeyCodeData() {KeyCode = 0x81, DisplayName = "Left Shift" },
        new KeyCodeData() {KeyCode = 0x82, DisplayName = "Left Alt" },
        new KeyCodeData() {KeyCode = 0x83, DisplayName = "Left Cmd/Win" },
        new KeyCodeData() {KeyCode = 0x84, DisplayName = "Right Ctrl" },
        new KeyCodeData() {KeyCode = 0x85, DisplayName = "Right Shift" },
        new KeyCodeData() {KeyCode = 0x86, DisplayName = "Right Alt" },
        new KeyCodeData() {KeyCode = 0x87, DisplayName = "Right Cmd/Win" },
        // special keys
        new KeyCodeData() {KeyCode = 0xB3, DisplayName = "TAB" },
        new KeyCodeData() {KeyCode = 0xC1, DisplayName = "Caps Lock" },
        new KeyCodeData() {KeyCode = 0xB2, DisplayName = "Backspace" },
        new KeyCodeData() {KeyCode = 0xB0, DisplayName = "Return" },
        new KeyCodeData() {KeyCode = 0xED, DisplayName = "Menu" },
        // navigation keys
        new KeyCodeData() {KeyCode = 0xD1, DisplayName = "Insert" },
        new KeyCodeData() {KeyCode = 0xD4, DisplayName = "Delete" },
        new KeyCodeData() {KeyCode = 0xD2, DisplayName = "Home" },
        new KeyCodeData() {KeyCode = 0xD5, DisplayName = "End" },
        new KeyCodeData() {KeyCode = 0xD3, DisplayName = "Page Up" },
        new KeyCodeData() {KeyCode = 0xD6, DisplayName = "Page Down" },
        new KeyCodeData() {KeyCode = 0xDA, DisplayName = "Up Arrow" },
        new KeyCodeData() {KeyCode = 0xD9, DisplayName = "Down Arrow" },
        new KeyCodeData() {KeyCode = 0xD8, DisplayName = "Left Arrow" },
        new KeyCodeData() {KeyCode = 0xD7, DisplayName = "Right Arrow" },
        // numpad
        new KeyCodeData() {KeyCode = 0xDB, DisplayName = "NumLock" },
        new KeyCodeData() {KeyCode = 0xDC, DisplayName = "Numpad /" },
        new KeyCodeData() {KeyCode = 0xDD, DisplayName = "Numpad *" },
        new KeyCodeData() {KeyCode = 0xDE, DisplayName = "Numpad -" },
        new KeyCodeData() {KeyCode = 0xDF, DisplayName = "Numpad +" },
        new KeyCodeData() {KeyCode = 0xE0, DisplayName = "Numpad Enter" },
        new KeyCodeData() {KeyCode = 0xE1, DisplayName = "Numpad 1" },
        new KeyCodeData() {KeyCode = 0xE2, DisplayName = "Numpad 2" },
        new KeyCodeData() {KeyCode = 0xE3, DisplayName = "Numpad 3" },
        new KeyCodeData() {KeyCode = 0xE4, DisplayName = "Numpad 4" },
        new KeyCodeData() {KeyCode = 0xE5, DisplayName = "Numpad 5" },
        new KeyCodeData() {KeyCode = 0xE6, DisplayName = "Numpad 6" },
        new KeyCodeData() {KeyCode = 0xE7, DisplayName = "Numpad 7" },
        new KeyCodeData() {KeyCode = 0xE8, DisplayName = "Numpad 8" },
        new KeyCodeData() {KeyCode = 0xE9, DisplayName = "Numpad 9" },
        new KeyCodeData() {KeyCode = 0xEA, DisplayName = "Numpad 0" },
        new KeyCodeData() {KeyCode = 0xEB, DisplayName = "Numpad dot" },
        // Escape and function keys
        new KeyCodeData() {KeyCode = 0xB1, DisplayName = "Escape" },
        new KeyCodeData() {KeyCode = 0xC2, DisplayName = "F1" },
        new KeyCodeData() {KeyCode = 0xC3, DisplayName = "F2" },
        new KeyCodeData() {KeyCode = 0xC4, DisplayName = "F3" },
        new KeyCodeData() {KeyCode = 0xC5, DisplayName = "F4" },
        new KeyCodeData() {KeyCode = 0xC6, DisplayName = "F5" },
        new KeyCodeData() {KeyCode = 0xC7, DisplayName = "F6" },
        new KeyCodeData() {KeyCode = 0xC8, DisplayName = "F7" },
        new KeyCodeData() {KeyCode = 0xC9, DisplayName = "F8" },
        new KeyCodeData() {KeyCode = 0xCA, DisplayName = "F9" },
        new KeyCodeData() {KeyCode = 0xCB, DisplayName = "F10" },
        new KeyCodeData() {KeyCode = 0xCC, DisplayName = "F11" },
        new KeyCodeData() {KeyCode = 0xCD, DisplayName = "F12" },
        new KeyCodeData() {KeyCode = 0xF0, DisplayName = "F13" },
        new KeyCodeData() {KeyCode = 0xF1, DisplayName = "F14" },
        new KeyCodeData() {KeyCode = 0xF2, DisplayName = "F15" },
        new KeyCodeData() {KeyCode = 0xF3, DisplayName = "F16" },
        new KeyCodeData() {KeyCode = 0xF4, DisplayName = "F17" },
        new KeyCodeData() {KeyCode = 0xF5, DisplayName = "F18" },
        new KeyCodeData() {KeyCode = 0xF6, DisplayName = "F19" },
        new KeyCodeData() {KeyCode = 0xF7, DisplayName = "F20" },
        new KeyCodeData() {KeyCode = 0xF8, DisplayName = "F21" },
        new KeyCodeData() {KeyCode = 0xF9, DisplayName = "F22" },
        new KeyCodeData() {KeyCode = 0xFA, DisplayName = "F23" },
        new KeyCodeData() {KeyCode = 0xFB, DisplayName = "F24" },
        // function control
        new KeyCodeData() {KeyCode = 0xCE, DisplayName = "PrintScreen" },
        new KeyCodeData() {KeyCode = 0xCF, DisplayName = "Scroll Lock" },
        new KeyCodeData() {KeyCode = 0xD0, DisplayName = "Pause/Break" },
    ];
} 