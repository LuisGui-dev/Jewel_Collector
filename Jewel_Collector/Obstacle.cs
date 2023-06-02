using System;
using Jewel_Collector.Enums;

namespace Jewel_Collector
{
    public class Obstacle : ICell
    {
        public ConsoleColor BackgroundColor => ConsoleColor.Black;
        public ConsoleColor ForegroundColor => ConsoleColor.Gray;
        public string Symbol { get; }
        public int EnergyPoints { get; }

        public Obstacle(ObstacleType type)
        {
            Symbol = GetSymbol(type);
            EnergyPoints = GetEnergyPoints(type);
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
        
        private int GetEnergyPoints(ObstacleType type)
        {
            switch (type)
            {
                case ObstacleType.Water:
                    return 0; // A água não fornece energia
                case ObstacleType.Tree:
                    return 3; // Cada árvore fornece 3 pontos de energia
                default:
                    return 0;
            }
        }
    }
}