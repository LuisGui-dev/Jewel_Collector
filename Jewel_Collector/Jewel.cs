using System;
using Jewel_Collector.Enums;
using Jewel_Collector.Interfaces;

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

        private static string GetSymbol(JewelType type)
        {
            return type switch
            {
                JewelType.Red => "JR",
                JewelType.Green => "JG",
                JewelType.Blue => "JB",
                _ => ""
            };
        }

        private static int GetPoints(JewelType type)
        {
            return type switch
            {
                JewelType.Red => 100,
                JewelType.Green => 50,
                JewelType.Blue => 10,
                _ => 0
            };
        }
    }
}