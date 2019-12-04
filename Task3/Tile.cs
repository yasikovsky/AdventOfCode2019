using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Dictionary<int, int> WiresOnTile { get; set; }
    }
}
