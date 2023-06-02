using System;

namespace Jewel_Collector
{
    public class Map
    {
        private readonly ICell[,] cells;
        public int Size { get; }

        public Map(int size)
        {
            Size = size;
            cells = new ICell[Size, Size];
            InitializeMap();
        }

        private void InitializeMap()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    cells[i, j] = new EmptyCell();
                }
            }
        }

        public ICell GetCell(int x, int y)
        {
            return cells[x, y];
        }

        public void SetCell(int x, int y, ICell cell)
        {
            cells[x, y] = cell;
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
                    ICell cell = cells[i, j];
                    Console.BackgroundColor = cell.BackgroundColor;
                    Console.ForegroundColor = cell.ForegroundColor;
                    Console.Write(cell.Symbol);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine("Energia do robô: " + robot.Energy);
            // Console.WriteLine("Total de joias coletadas: " + robot);
            Console.WriteLine("Estado da sacola do robô: " + robot.Score);
        }
    }
}