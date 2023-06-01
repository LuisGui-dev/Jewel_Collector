using System;
using Jewel_Collector.Enums;

namespace Jewel_Collector
{
    public class Jewel : ICell
    {
        public ConsoleColor BackgroundColor => ConsoleColor.Black;
        public ConsoleColor ForegroundColor => ConsoleColor.Yellow;
        public string Symbol { get; }

        public int Points { get; }

        public Jewel(JewelType type)
        {
            Symbol = GetSymbol(type);
            Points = GetPoints(type);
        }

        private string GetSymbol(JewelType type)
        {
            switch (type)
            {
                case JewelType.Red:
                    return "JR";
                case JewelType.Green:
                    return "JG";
                case JewelType.Blue:
                    return "JB";
                default:
                    return "";
            }
        }

        private int GetPoints(JewelType type)
        {
            switch (type)
            {
                case JewelType.Red:
                    return 100;
                case JewelType.Green:
                    return 50;
                case JewelType.Blue:
                    return 10;
                default:
                    return 0;
            }
        }
    }
}