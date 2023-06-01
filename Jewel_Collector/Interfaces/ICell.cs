using System;

namespace Jewel_Collector
{
    public interface ICell
    {
        ConsoleColor BackgroundColor { get; }
        ConsoleColor ForegroundColor { get; }
        string Symbol { get; }
    }
}