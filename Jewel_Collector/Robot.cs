using System;
using System.Collections.Generic;
using Jewel_Collector.Exceptions;
using Jewel_Collector.Interfaces;

namespace Jewel_Collector
{
    public class Robot : ICell
    {
        private readonly Map map;
        private readonly List<Jewel> Bag;
        private int Score { get; set; }

        public ConsoleColor BackgroundColor => ConsoleColor.Black;
        public ConsoleColor ForegroundColor => ConsoleColor.Magenta;
        public string Symbol { get; } = "ME";

        public int X { get; set; }
        public int Y { get; set; }
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

            var destinationCell = map.GetCell(newX, newY);

            if (Energy == 0)
            {
                Console.WriteLine("A energia do robô acabou. O jogo terminou.");
                Environment.Exit(0);
            }

            if (!map.IsWithinBounds(newX, newY)) return;
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

        public void PrintTotalJewels()
        {
            Console.WriteLine("Total de joias coletadas: " + Bag.Count);
            Console.WriteLine("Valor total das joias coletadas: " + Score);
        }

        public void InteractWithAdjacentItems()
        {
            CollectJewel();
            var adjacentPositions = GetAdjacentPositions();
            foreach (var (adjX, adjY) in adjacentPositions)
            {
                if (!map.IsWithinBounds(adjX, adjY)) continue;
                var cell = map.GetCell(adjX, adjY);
                switch (cell)
                {
                    case Jewel:
                        map.SetCell(adjX, adjY, new EmptyCell());
                        break;
                    case Obstacle obstacle:
                        RechargeEnergy(obstacle);
                        break;
                }
            }
        }

        public List<(int, int)> GetAdjacentPositions()
        {
            var positions = new List<(int, int)>
            {
                (X - 1, Y), // Cima
                (X + 1, Y), // Baixo
                (X, Y - 1), // Esquerda
                (X, Y + 1) // Direita
            };
            return positions;
        }

        private void CollectJewel()
        {
            var adjacentPositions = GetAdjacentPositions();
            foreach (var (adjX, adjY) in adjacentPositions)
            {
                if (!map.IsWithinBounds(adjX, adjY) || map.GetCell(adjX, adjY) is not Jewel jewel) continue;
                Score += jewel.Points;
                Bag.Add(jewel);

                if (jewel.Symbol == "JB")
                {
                    Energy += 5; // Adiciona 5 pontos de energia para a joia azul
                }

                map.SetCell(adjX, adjY, new EmptyCell());
                return;
            }

            foreach (var (adjX, adjY) in adjacentPositions)
            {
                if (!map.IsWithinBounds(adjX, adjY)) continue;
                var cell = map.GetCell(adjX, adjY);
                if (cell is not Radioactive) continue;
                var penalty = Math.Min(30, Energy); // Calcula a penalidade mínima de energia
                Energy -= penalty;
                map.SetCell(adjX, adjY, new EmptyCell());
            }
        }

        private void RechargeEnergy(Obstacle obstacle)
        {
            Energy += obstacle.EnergyPoints;
        }

        public void LoseEnergy(int amount)
        {
            Energy -= amount;
        }

        public void TransposeRadioactive(int newX, int newY)
        {
            if (Energy < 30) return;
            Energy -= 30;
            map.SetCell(newX, newY, new EmptyCell());
        }

        public Map GetMap()
        {
            return map;
        }
    }
}