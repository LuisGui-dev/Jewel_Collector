using System;

namespace Jewel_Collector
{
    public class Radioactive : ICell
    {
        public ConsoleColor BackgroundColor => ConsoleColor.Black;
        public ConsoleColor ForegroundColor => ConsoleColor.Magenta;
        public string Symbol { get; } = "!!";
        public int EnergyPoints { get; } = -10;
    }
}