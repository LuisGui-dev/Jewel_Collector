using System;
using Jewel_Collector.Interfaces;

namespace Jewel_Collector
{
    public class EmptyCell : ICell
    {
        public ConsoleColor BackgroundColor => ConsoleColor.Black;
        public ConsoleColor ForegroundColor => ConsoleColor.White;
        public string Symbol { get; } = "--";
    }
}