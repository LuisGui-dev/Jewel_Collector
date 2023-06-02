using System;
using Jewel_Collector.Enums;

namespace Jewel_Collector
{
    public class Map
    {
       private ICell[,] Cells { get; set; }
        public int Size { get; set; }

        public Map(int size)
        {
            Size = size;
            Cells = new ICell[Size, Size];
            InitializeMap();
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
        
        public void RandomizeItems(int fase)
        {
            ClearMap();
            RandomizeJewels(fase);
            RandomizeObstacles(fase);
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
        
        private void RandomizeJewels(int fase)
        {
            int numJewels = fase * 3; // Aumenta a quantidade de joias proporcionalmente ao número da fase
            Random random = new Random();

            for (int i = 0; i < numJewels; i++)
            {
                int x = random.Next(Size);
                int y = random.Next(Size);

                if (Cells[x, y] is EmptyCell)
                {
                    JewelType randomType = (JewelType)random.Next(3); // Gera um tipo de joia aleatório
                    Cells[x, y] = new Jewel(randomType);
                }
                else
                {
                    i--; // Tentar novamente se a célula não estiver vazia
                }
            }
        }
        
        private void RandomizeObstacles(int fase)
        {
            int numObstacles = fase * 3; // Aumenta a quantidade de obstáculos proporcionalmente ao número da fase
            Random random = new Random();

            for (int i = 0; i < numObstacles; i++)
            {
                int x = random.Next(Size);
                int y = random.Next(Size);

                if (Cells[x, y] is EmptyCell)
                {
                    ObstacleType randomType = (ObstacleType)random.Next(2); // Gera um tipo de obstáculo aleatório
                    Cells[x, y] = new Obstacle(randomType);
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
    }
}