using System;
using Jewel_Collector.Enums;

namespace Jewel_Collector
{
    public class Map
    {
        private ICell[,] Cells { get; set; }
        public int Size { get; set; }
        public int Phase { get; set; }

        public Map(int size)
        {
            Size = size;
            Cells = new ICell[Size, Size];
            InitializeMap();
            Phase = 1;
        }

        private void InitializeMap()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
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
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    ICell cell = Cells[i, j];
                    Console.BackgroundColor = cell.BackgroundColor;
                    Console.ForegroundColor = cell.ForegroundColor;

                    if (cell is Jewel jewel)
                    {
                        switch (jewel.Symbol)
                        {
                            case "JR":
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case "JG":
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case "JB":
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                        }

                        Console.Write(jewel.Symbol);
                    }
                    else if (cell is Obstacle obstacle)
                    {
                        switch (obstacle.Symbol)
                        {
                            case "##":
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case "$$":
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                        }

                        Console.Write(obstacle.Symbol);
                    }
                    else
                    {
                        Console.Write(cell.Symbol);
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
            int totalJewels = 0;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
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
            if (Size < 30)
            {
                Size++;
                Cells = new ICell[Size, Size];
                InitializeMap();
            }
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
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Cells[i, j] = new EmptyCell();
                }
            }
        }

        private void RandomizeJewels()
        {
            int maxJewels = (int)Math.Round(0.06 * Size * Size); // Define a quantidade máxima de joias proporcional ao tamanho do mapa
            int numJewels = Math.Min(maxJewels, Size * Size); // Limita o número de joias ao tamanho do mapa
            Random random = new Random();

            for (int i = 0; i < numJewels; i++)
            {
                int x = random.Next(Size);
                int y = random.Next(Size);

                if (Cells[x, y] is EmptyCell)
                {
                    JewelType[] jewelTypes = Enum.GetValues(typeof(JewelType)) as JewelType[];
                    JewelType randomType = jewelTypes[random.Next(jewelTypes.Length)]; // Gera um tipo de joia aleatório

                    Jewel jewel = new Jewel(randomType);
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
            int maxObstacles = (int)Math.Round(0.12 * Size * Size);// Define a quantidade máxima de obstáculos proporcional ao tamanho do mapa
            int numObstacles = Math.Min(maxObstacles, Size * Size); // Limita o número de obstáculos ao tamanho do mapa
            Random random = new Random();

            for (int i = 0; i < numObstacles; i++)
            {
                int x = random.Next(Size);
                int y = random.Next(Size);

                if (Cells[x, y] is EmptyCell)
                {
                    ObstacleType[] obstacleTypes = Enum.GetValues(typeof(ObstacleType)) as ObstacleType[];
                    ObstacleType randomType = obstacleTypes[random.Next(obstacleTypes.Length)]; // Gera um tipo de obstáculo aleatório

                    Obstacle obstacle = new Obstacle(randomType);
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
            Random random = new Random();

            int x = random.Next(Size);
            int y = random.Next(Size);

            if (Cells[x, y] is EmptyCell)
            {
                Cells[x, y] = new Radioactive();
            }
            else
            {
                RandomizeRadioactive(); // Tentar novamente se a célula não estiver vazia
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