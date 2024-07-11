using System;

namespace extrakeys.Models;

public record KeyBinding
{
    public required int KeyNumber { get; init; }
    public required KeyCodeData[] KeyCodes { get; init; }
    
    
}


