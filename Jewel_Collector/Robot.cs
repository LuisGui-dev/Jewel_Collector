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
            CollectJewel();
            List<(int, int)> adjacentPositions = GetAdjacentPositions();
            foreach ((int adjX, int adjY) in adjacentPositions)
            {
                if (map.IsWithinBounds(adjX, adjY))
                {
                    ICell cell = map.GetCell(adjX, adjY);
                    if (cell is Jewel jewel)
                    {
                        map.SetCell(adjX, adjY, new EmptyCell());
                    }
                    else if (cell is Obstacle obstacle)
                    {
                        RechargeEnergy(obstacle);
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
        
        private void CollectJewel()
        {
            List<(int, int)> adjacentPositions = GetAdjacentPositions();
            foreach ((int adjX, int adjY) in adjacentPositions)
            {
                if (map.IsWithinBounds(adjX, adjY) && map.GetCell(adjX, adjY) is Jewel jewel)
                {
                    Score += jewel.Points;

                    if (jewel.Symbol == "JB")
                    {
                        Energy += 5; // Adiciona 5 pontos de energia para a joia azul
                    }

                    map.SetCell(adjX, adjY, new EmptyCell());
                    return;
                }
            }
        }
        
        private void RechargeEnergy(Obstacle obstacle)
        {
            Energy += obstacle.EnergyPoints;
        }
    }
}