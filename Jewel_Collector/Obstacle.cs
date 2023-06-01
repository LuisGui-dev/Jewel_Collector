using System;
using Jewel_Collector.Enums;

namespace Jewel_Collector
{
    public class Obstacle : ICell
    {
        public ConsoleColor BackgroundColor => ConsoleColor.Black;
        public ConsoleColor ForegroundColor => ConsoleColor.Gray;
        public string Symbol { get; }

        public Obstacle(ObstacleType type)
        {
            Symbol = GetSymbol(type);
        }

        private string GetSymbol(ObstacleType type)
        {
            switch (type)
            {
                case ObstacleType.Water:
                    return "##";
                case ObstacleType.Tree:
                    return "$$";
                default:
                    return "";
            }
        }
    }
}