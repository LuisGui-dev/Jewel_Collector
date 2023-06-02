using System;
using System.Collections.Generic;
using Jewel_Collector.Exceptions;

namespace Jewel_Collector
{
    public class Robot : ICell
    {
        private readonly Map map;
        private readonly List<Jewel> Bag;
        
        public ConsoleColor BackgroundColor => ConsoleColor.Black;
        public ConsoleColor ForegroundColor => ConsoleColor.Magenta;
        public string Symbol { get; } = "ME";

        public int X { get; set; }
        public int Y { get; set; }
        public int Score { get; set; }
        public int Energy { get; private set; } = 5;
        

        public Robot(int x, int y, Map map)
        {
            X = x;
            Y = y;
            Score = 0;
            this.map = map;
            Bag = new List<Jewel>();
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

        public void PrintTotalJewels()
        {
            Console.WriteLine("Total de joias coletadas: " + Bag.Count);
            Console.WriteLine("Valor total das joias coletadas: " + Score);
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
                    Bag.Add(jewel);

                    if (jewel.Symbol == "JB")
                    {
                        Energy += 5; // Adiciona 5 pontos de energia para a joia azul
                    }

                    map.SetCell(adjX, adjY, new EmptyCell());
                    return;
                }
            }
            
            foreach ((int adjX, int adjY) in adjacentPositions)
            {
                if (map.IsWithinBounds(adjX, adjY))
                {
                    ICell cell = map.GetCell(adjX, adjY);
                    if (cell is Radioactive radioactive)
                    {
                        Console.WriteLine("Cuidado! Você encontrou um elemento radioativo!");
                        int penalty = Math.Max(30, Energy); // Calcula a penalidade mínima de energia
                        Energy -= penalty;
                        map.SetCell(adjX, adjY, new EmptyCell());
                    }
                }
            }
        }
        
        private void RechargeEnergy(Obstacle obstacle)
        {
            Energy += obstacle.EnergyPoints;
        }
        
        private void TransposeRadioactive(int newX, int newY)
        {
            if (Energy >= 30)
            {
                Energy -= 30;
                Console.WriteLine("Você transpôs o elemento radioativo, perdendo 30 pontos de energia.");
                map.SetCell(newX, newY, new EmptyCell());
            }
            else
            {
                Console.WriteLine("Você não tem energia suficiente para transpor o elemento radioativo!");
            }
        }
    }
}