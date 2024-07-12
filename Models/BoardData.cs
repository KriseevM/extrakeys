using System.IO.Ports;

namespace extrakeys.Models;

public record BoardData
{
    public required SerialPort Port { get; init; }
    public required uint ButtonRows { get; init; }
    public required uint ButtonCols { get; init; }
    public required uint MacroCount { get; init; }

    public override string ToString()
    {
        return $"{Port.PortName}: {ButtonCols}x{ButtonRows} ({MacroCount})";
    }
}