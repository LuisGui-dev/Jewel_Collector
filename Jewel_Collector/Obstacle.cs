using System;
using Jewel_Collector.Enums;
using Jewel_Collector.Interfaces;

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

        private static string GetSymbol(ObstacleType type)
        {
            return type switch
            {
                ObstacleType.Water => "##",
                ObstacleType.Tree => "$$",
                _ => ""
            };
        }
        
        private static int GetEnergyPoints(ObstacleType type)
        {
            return type switch
            {
                ObstacleType.Water => 0 // A água não fornece energia
                ,
                ObstacleType.Tree => 3 // Cada árvore fornece 3 pontos de energia
                ,
                _ => 0
            };
        }
    }
}