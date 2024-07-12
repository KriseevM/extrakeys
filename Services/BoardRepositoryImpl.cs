using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using DynamicData;
using extrakeys.Lang;
using extrakeys.Models;
using extrakeys.Services.Interfaces;

namespace extrakeys.Services;

public class BoardRepositoryImpl : IBoardRepository
{
    public ObservableCollection<BoardData> Boards { get; }

    public void Refresh()
    {
        LookupBoards();
    }

    public BoardRepositoryImpl()
    {
        Boards = new ObservableCollection<BoardData>();
        LookupBoards();
    }

    private void LookupBoards()
    {
        Console.WriteLine(Resources.BoardRepository_begin_lookup);
        var boardList = new List<BoardData>();
        string[] ports = SerialPort.GetPortNames();
        foreach (var port in ports)
        {
            var p = new SerialPort(port, 19200);
            try
            {
                p.DtrEnable = true;
                p.RtsEnable = true;
                p.Open();
                p.Write(new byte[] { 0xAD }, 0, 1);
                var data = p.ReadLine()?.Split(' ');
                if (data != null)
                {
                    var width = uint.Parse(data[0]);
                    var height = uint.Parse(data[1]);
                    var macroCount = uint.Parse(data[2]);
                    boardList.Add(new BoardData()
                    {
                        Port = p,
                        ButtonCols = width,
                        ButtonRows = height,
                        MacroCount = macroCount,
                    });
                }
                Console.WriteLine(Resources.BoardRepository_lookup_found, boardList.Last());
            }
            catch (Exception e)
            {
                // ignored
                Console.WriteLine(Resources.BoardRepository_lookup_invalid_port, p.PortName, e.GetType(), e.Message);
            }
            finally
            {
                Console.WriteLine(Resources.BoardRepository_finish_lookup);
                p.Close();
            }
        }
        Boards.Clear();
        Boards.AddRange(boardList);
        
    }
}