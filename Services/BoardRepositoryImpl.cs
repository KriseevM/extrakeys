using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Ports;
using extrakeys.Models;
using extrakeys.Services.Interfaces;

namespace extrakeys.Services;

public class BoardRepositoryImpl : IBoardRepository
{
    public List<BoardData> BoardList { get; }

    public BoardRepositoryImpl()
    {
        BoardList = new List<BoardData>();
        string[] ports = SerialPort.GetPortNames();
        foreach (var port in ports)
        {
            SerialPort p = new SerialPort(port, 19200);
            p.Open();
            p.Write(new byte[] { 0xAD }, 0, 1);
            try
            {
                var data = p.ReadLine()?.Split(' ');
                (uint width, uint height, uint macroCount) =
                    (uint.Parse(data?[0]!), uint.Parse(data[1]), uint.Parse(data[2]));
                BoardList.Add(new BoardData()
                {
                    Port = p,
                    ButtonCols = width,
                    ButtonRows = height,
                    MacroCount = macroCount,
                });
            }
            catch (Exception)
            {
                // ignored
            }

            p.Close();
        }
    }
}