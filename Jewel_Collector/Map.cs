using System;
using Jewel_Collector.Enums;
using Jewel_Collector.Interfaces;

namespace Jewel_Collector
{
    public class Map
    {
        private ICell[,] Cells { get; set; }
        private int Size { get; set; }
        private int Phase { get; set; }

        public Map(int size)
        {
            Size = size;
            Cells = new ICell[Size, Size];
            InitializeMap();
            Phase = 1;
        }

        private void InitializeMap()
        {
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    Cells[i, j] = new EmptyCell();
                }
            }
        }

        public ICell GetCell(int x, int y)
        {
            return Cells[x, y];
        }

        public void SetCell(int x, int y, ICell cell)
        {
            Cells[x, y] = cell;
        }

        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }

        public void PrintMap(Robot robot)
        {
            Console.Clear();
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    var cell = Cells[i, j];
                    Console.BackgroundColor = cell.BackgroundColor;
                    Console.ForegroundColor = cell.ForegroundColor;

                    switch (cell)
                    {
                        case Jewel jewel:
                            Console.ForegroundColor = jewel.Symbol switch
                            {
                                "JR" => ConsoleColor.Red,
                                "JG" => ConsoleColor.Green,
                                "JB" => ConsoleColor.Blue,
                                _ => Console.ForegroundColor
                            };

                            Console.Write(jewel.Symbol);
                            break;
                        case Obstacle obstacle:
                            Console.ForegroundColor = obstacle.Symbol switch
                            {
                                "##" => ConsoleColor.Cyan,
                                "$$" => ConsoleColor.Yellow,
                                _ => Console.ForegroundColor
                            };

                            Console.Write(obstacle.Symbol);
                            break;
                        default:
                            Console.Write(cell.Symbol);
                            break;
                    }

                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.WriteLine("Energia do robô: " + robot.Energy);
            robot.PrintTotalJewels();
        }

        public int GetTotalJewels()
        {
            var totalJewels = 0;

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (Cells[i, j] is Jewel)
                    {
                        totalJewels++;
                    }
                }
            }

            return totalJewels;
        }

        public void IncreaseSize()
        {
            if (Size >= 30) return;
            Size++;
            Cells = new ICell[Size, Size];
            InitializeMap();
        }

        public void RandomizeItems()
        {
            ClearMap();
            RandomizeJewels();
            RandomizeObstacles();
            RandomizeRadioactive();
        }

        private void ClearMap()
        {
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    Cells[i, j] = new EmptyCell();
                }
            }
        }

        private void RandomizeJewels()
        {
            var maxJewels = (int)Math.Round(0.06 * Size * Size); // Define a quantidade máxima de joias proporcional ao tamanho do mapa
            var numJewels = Math.Min(maxJewels, Size * Size); // Limita o número de joias ao tamanho do mapa
            var random = new Random();

            for (var i = 0; i < numJewels; i++)
            {
                var x = random.Next(Size);
                var y = random.Next(Size);

                if (Cells[x, y] is EmptyCell)
                {
                    var jewelTypes = Enum.GetValues(typeof(JewelType)) as JewelType[];
                    if (jewelTypes == null) continue;
                    var randomType = jewelTypes[random.Next(jewelTypes.Length)]; // Gera um tipo de joia aleatório

                    var jewel = new Jewel(randomType);
                    Cells[x, y] = jewel; // Atribuir a joia gerada à célula
                }
                else
                {
                    i--; // Tentar novamente se a célula não estiver vazia
                }
            }
        }

        private void RandomizeObstacles()
        {
            var maxObstacles = (int)Math.Round(0.12 * Size * Size); // Define a quantidade máxima de obstáculos proporcional ao tamanho do mapa
            var numObstacles = Math.Min(maxObstacles, Size * Size); // Limita o número de obstáculos ao tamanho do mapa
            var random = new Random();

            for (var i = 0; i < numObstacles; i++)
            {
                var x = random.Next(Size);
                var y = random.Next(Size);

                if (Cells[x, y] is EmptyCell)
                {
                    var obstacleTypes = Enum.GetValues(typeof(ObstacleType)) as ObstacleType[];
                    if (obstacleTypes == null) continue;
                    var randomType = obstacleTypes[random.Next(obstacleTypes.Length)]; // Gera um tipo de obstáculo aleatório

                    var obstacle = new Obstacle(randomType);
                    Cells[x, y] = obstacle; // Atribuir o obstáculo gerado à célula
                }
                else
                {
                    i--; // Tentar novamente se a célula não estiver vazia
                }
            }
        }

        private void RandomizeRadioactive()
        {
            while (true)
            {
                var random = new Random();

                var x = random.Next(Size);
                var y = random.Next(Size);

                if (Cells[x, y] is EmptyCell)
                {
                    Cells[x, y] = new Radioactive();
                }
                else
                {
                    continue;
                }

                break;
            }
        }

        public int GetPhase()
        {
            return Phase;
        }

        public void IncrementPhase()
        {
            Phase++;
        }
    }
}