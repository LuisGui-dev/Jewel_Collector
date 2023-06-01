using System;
using System.Collections.Generic;

namespace Jewel_Collector
{
    public class Robot : ICell
    {
        public ConsoleColor BackgroundColor => ConsoleColor.Black;
        public ConsoleColor ForegroundColor => ConsoleColor.Cyan;
        public string Symbol { get; } = "ME";

        public int X { get; set; }
        public int Y { get; set; }
        public int Score { get; set; }

        private readonly Map map;

        public Robot(int x, int y, Map map)
        {
            X = x;
            Y = y;
            Score = 0;
            this.map = map;
            map.SetCell(x, y, this);
        }

        public void Move(int newX, int newY)
        {
            if (map.IsWithinBounds(newX, newY) && map.GetCell(newX, newY) is EmptyCell)
            {
                map.SetCell(X, Y, new EmptyCell());
                X = newX;
                Y = newY;
                map.SetCell(X, Y, this);
            }
        }

        public void CollectJewel()
        {
            List<(int, int)> adjacentPositions = GetAdjacentPositions();
            foreach ((int adjX, int adjY) in adjacentPositions)
            {
                if (map.IsWithinBounds(adjX, adjY) && map.GetCell(adjX, adjY) is Jewel jewel)
                {
                    Score += jewel.Points;
                    map.SetCell(adjX, adjY, new EmptyCell());
                    return;
                }
            }
        }

        private List<(int, int)> GetAdjacentPositions()
        {
            List<(int, int)> positions = new List<(int, int)>();
            positions.Add((X - 1, Y)); // Cima
            positions.Add((X + 1, Y)); // Baixo
            positions.Add((X, Y - 1)); // Esquerda
            positions.Add((X, Y + 1)); // Direita
            return positions;
        }
    }
}