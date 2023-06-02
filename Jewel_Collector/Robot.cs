using System;
using System.Collections.Generic;
using Jewel_Collector.Exceptions;

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
        public int Energy { get; private set; } = 5;

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
            if (!map.IsWithinBounds(newX, newY))
            {
                throw new OutOfMapBoundsException();
            }
            
            ICell destinationCell = map.GetCell(newX, newY);
            
            if (Energy == 0)
            {
                Console.WriteLine("A energia do robô acabou. O jogo terminou.");
                Environment.Exit(0);
            }
            
            if (map.IsWithinBounds(newX, newY))
            {
                if (destinationCell is EmptyCell)
                {
                    map.SetCell(X, Y, new EmptyCell());
                    X = newX;
                    Y = newY;
                    map.SetCell(X, Y, this);
                    Energy--; // Reduz a energia em 1 após o movimento
                    
                }
                else
                {
                    throw new InvalidMoveException();
                }
            }
        }
        
        public void InteractWithAdjacentItems()
        {
            List<(int, int)> adjacentPositions = GetAdjacentPositions();
            foreach ((int adjX, int adjY) in adjacentPositions)
            {
                if (map.IsWithinBounds(adjX, adjY))
                {
                    ICell adjacentCell = map.GetCell(adjX, adjY);

                    if (adjacentCell is Jewel jewel)
                    {
                        Score += jewel.Points;
                        map.SetCell(adjX, adjY, new EmptyCell());
                        Energy = Math.Min(5, Energy + jewel.Points); // Recarrega a energia com base na pontuação da joia
                    }
                    else if (adjacentCell is Obstacle obstacle)
                    {
                        Energy = Math.Min(5, Energy + 3); // Recarrega 3 pontos de energia ao encontrar um obstáculo
                    }
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