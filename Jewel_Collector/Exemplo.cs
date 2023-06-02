using System;

namespace Jewel_Collector
{
    // public class Exemplo
    // {
    //     public enum JewelType
    //     {
    //         Red = 100,
    //         Green = 50,
    //         Blue = 10
    //     }
    //
    //     public enum ObstacleType
    //     {
    //         Water,
    //         Tree
    //     }
    //
    //     public class InvalidMoveException : Exception
    //     {
    //         public InvalidMoveException() : base("Movimento inválido. A posição está ocupada por outro item.")
    //         {
    //         }
    //     }
    //
    //     public class OutOfMapBoundsException : Exception
    //     {
    //         public OutOfMapBoundsException() : base("A posição está fora dos limites do mapa.")
    //         {
    //         }
    //     }
    //
    //     public interface ICell
    //     {
    //         ConsoleColor BackgroundColor { get; }
    //         ConsoleColor ForegroundColor { get; }
    //         string Symbol { get; }
    //     }
    //
    //     public class EmptyCell : ICell
    //     {
    //         public ConsoleColor BackgroundColor => ConsoleColor.Black;
    //         public ConsoleColor ForegroundColor => ConsoleColor.White;
    //         public string Symbol { get; } = "--";
    //     }
    //
    //     public class Jewel : ICell
    //     {
    //         public ConsoleColor BackgroundColor => ConsoleColor.Black;
    //         public ConsoleColor ForegroundColor => ConsoleColor.Yellow;
    //         public string Symbol { get; }
    //
    //         public int Points { get; }
    //
    //         public Jewel(JewelType type)
    //         {
    //             Symbol = GetSymbol(type);
    //             Points = GetPoints(type);
    //         }
    //
    //         private string GetSymbol(JewelType type)
    //         {
    //             switch (type)
    //             {
    //                 case JewelType.Red:
    //                     return "JR";
    //                 case JewelType.Green:
    //                     return "JG";
    //                 case JewelType.Blue:
    //                     return "JB";
    //                 default:
    //                     return "";
    //             }
    //         }
    //
    //         private int GetPoints(JewelType type)
    //         {
    //             switch (type)
    //             {
    //                 case JewelType.Red:
    //                     return 100;
    //                 case JewelType.Green:
    //                     return 50;
    //                 case JewelType.Blue:
    //                     return 10;
    //                 default:
    //                     return 0;
    //             }
    //         }
    //
    //         public class JewelCollector
    //         {
    //             public static void Main()
    //             {
    //                 Map map = new Map(10);
    //
    //                 map.SetCell(1, 9, new Jewel(JewelType.Red));
    //                 map.SetCell(8, 8, new Jewel(JewelType.Red));
    //                 map.SetCell(9, 1, new Jewel(JewelType.Green));
    //                 map.SetCell(7, 6, new Jewel(JewelType.Green));
    //                 map.SetCell(3, 4, new Jewel(JewelType.Blue));
    //                 map.SetCell(2, 1, new Jewel(JewelType.Blue));
    //
    //                 map.SetCell(5, 0, new Obstacle(ObstacleType.Water));
    //                 map.SetCell(5, 1, new Obstacle(ObstacleType.Water));
    //                 map.SetCell(5, 2, new Obstacle(ObstacleType.Water));
    //                 map.SetCell(5, 3, new Obstacle(ObstacleType.Water));
    //                 map.SetCell(5, 4, new Obstacle(ObstacleType.Water));
    //                 map.SetCell(5, 5, new Obstacle(ObstacleType.Water));
    //                 map.SetCell(5, 6, new Obstacle(ObstacleType.Water));
    //                 map.SetCell(5, 9, new Obstacle(ObstacleType.Tree));
    //                 map.SetCell(3, 9, new Obstacle(ObstacleType.Tree));
    //                 map.SetCell(8, 3, new Obstacle(ObstacleType.Tree));
    //                 map.SetCell(2, 5, new Obstacle(ObstacleType.Tree));
    //                 map.SetCell(1, 4, new Obstacle(ObstacleType.Tree));
    //
    //                 Robot robot = new Robot(0, 0, map);
    //
    //                 while (true)
    //                 {
    //                     map.PrintMap(robot);
    //                     ConsoleKeyInfo key = Console.ReadKey(true);
    //
    //                     switch (key.KeyChar)
    //                     {
    //                         case 'w':
    //                             HandleRobotMovement(robot, robot.X - 1, robot.Y);
    //                             break;
    //                         case 's':
    //                             HandleRobotMovement(robot, robot.X + 1, robot.Y);
    //                             break;
    //                         case 'a':
    //                             HandleRobotMovement(robot, robot.X, robot.Y - 1);
    //                             break;
    //                         case 'd':
    //                             HandleRobotMovement(robot, robot.X, robot.Y + 1);
    //                             break;
    //                         case 'g':
    //                             robot.InteractWithAdjacentItems();
    //                             break;
    //                         default:
    //                             return;
    //                     }
    //                 }
    //             }
    //
    //             private static void HandleRobotMovement(Robot robot, int newX, int newY)
    //             {
    //                 try
    //                 {
    //                     robot.Move(newX, newY);
    //                 }
    //                 catch (OutOfMapBoundsException)
    //                 {
    //                     Console.WriteLine("ERRO: A posição está fora dos limites do mapa.");
    //                     throw;
    //                 }
    //                 catch (InvalidMoveException)
    //                 {
    //                     Console.WriteLine("ERRO: Movimento inválido. A posição está ocupada por outro item.");
    //                     throw;
    //                 }
    //             }
    //         }
    //     }
    //     
    //     public class Map
    // {
    //     private readonly ICell[,] cells;
    //     public int Size { get; }
    //
    //     public Map(int size)
    //     {
    //         Size = size;
    //         cells = new ICell[Size, Size];
    //         InitializeMap();
    //     }
    //
    //     private void InitializeMap()
    //     {
    //         for (int i = 0; i < Size; i++)
    //         {
    //             for (int j = 0; j < Size; j++)
    //             {
    //                 cells[i, j] = new EmptyCell();
    //             }
    //         }
    //     }
    //
    //     public ICell GetCell(int x, int y)
    //     {
    //         return cells[x, y];
    //     }
    //
    //     public void SetCell(int x, int y, ICell cell)
    //     {
    //         cells[x, y] = cell;
    //     }
    //
    //     public bool IsWithinBounds(int x, int y)
    //     {
    //         return x >= 0 && x < Size && y >= 0 && y < Size;
    //     }
    //
    //     public void PrintMap(Robot robot)
    //     {
    //         Console.Clear();
    //         for (int i = 0; i < Size; i++)
    //         {
    //             for (int j = 0; j < Size; j++)
    //             {
    //                 ICell cell = cells[i, j];
    //                 Console.BackgroundColor = cell.BackgroundColor;
    //                 Console.ForegroundColor = cell.ForegroundColor;
    //
    //                 if (cell is Jewel jewel)
    //                 {
    //                     switch (jewel.Symbol)
    //                     {
    //                         case "JR":
    //                             Console.ForegroundColor = ConsoleColor.Red;
    //                             break;
    //                         case "JG":
    //                             Console.ForegroundColor = ConsoleColor.Green;
    //                             break;
    //                         case "JB":
    //                             Console.ForegroundColor = ConsoleColor.Blue;
    //                             break;
    //                     }
    //                     
    //                     Console.Write(jewel.Symbol);
    //                 }
    //                 else if (cell is Obstacle obstacle)
    //                 {
    //                     switch (obstacle.Symbol)
    //                     {
    //                         case "##":
    //                             Console.ForegroundColor = ConsoleColor.Cyan;
    //                             break;
    //                         case "$$":
    //                             Console.ForegroundColor = ConsoleColor.Yellow;
    //                             break;
    //                     }
    //                     
    //                     Console.Write(obstacle.Symbol);
    //                 }
    //                 else
    //                 {
    //                     Console.Write(cell.Symbol);
    //                 }
    //                 
    //                 Console.ResetColor();
    //             }
    //             Console.WriteLine();
    //         }
    //         Console.WriteLine("Energia do robô: " + robot.Energy);
    //         robot.PrintTotalJewels();
    //     }
    // }
    //     
    //     public class Obstacle : ICell
    //     {
    //         public ConsoleColor BackgroundColor => ConsoleColor.Black;
    //         public ConsoleColor ForegroundColor => ConsoleColor.Gray;
    //         public string Symbol { get; }
    //         public int EnergyPoints { get; }
    //
    //         public Obstacle(ObstacleType type)
    //         {
    //             Symbol = GetSymbol(type);
    //             EnergyPoints = GetEnergyPoints(type);
    //         }
    //
    //         private string GetSymbol(ObstacleType type)
    //         {
    //             switch (type)
    //             {
    //                 case ObstacleType.Water:
    //                     return "##";
    //                 case ObstacleType.Tree:
    //                     return "$$";
    //                 default:
    //                     return "";
    //             }
    //         }
    //     
    //         private int GetEnergyPoints(ObstacleType type)
    //         {
    //             switch (type)
    //             {
    //                 case ObstacleType.Water:
    //                     return 0; // A água não fornece energia
    //                 case ObstacleType.Tree:
    //                     return 3; // Cada árvore fornece 3 pontos de energia
    //                 default:
    //                     return 0;
    //             }
    //         }
    //     }
    //     
    //     public class Robot : ICell
    // {
    //     private readonly Map map;
    //     private readonly List<Jewel> Bag;
    //     
    //     public ConsoleColor BackgroundColor => ConsoleColor.Black;
    //     public ConsoleColor ForegroundColor => ConsoleColor.Magenta;
    //     public string Symbol { get; } = "ME";
    //
    //     public int X { get; set; }
    //     public int Y { get; set; }
    //     public int Score { get; set; }
    //     public int Energy { get; private set; } = 5;
    //     
    //
    //     public Robot(int x, int y, Map map)
    //     {
    //         X = x;
    //         Y = y;
    //         Score = 0;
    //         this.map = map;
    //         Bag = new List<Jewel>();
    //         map.SetCell(x, y, this);
    //     }
    //
    //     public void Move(int newX, int newY)
    //     {
    //         if (!map.IsWithinBounds(newX, newY))
    //         {
    //             throw new OutOfMapBoundsException();
    //         }
    //         
    //         ICell destinationCell = map.GetCell(newX, newY);
    //         
    //         if (Energy == 0)
    //         {
    //             Console.WriteLine("A energia do robô acabou. O jogo terminou.");
    //             Environment.Exit(0);
    //         }
    //         
    //         if (map.IsWithinBounds(newX, newY))
    //         {
    //             if (destinationCell is EmptyCell)
    //             {
    //                 map.SetCell(X, Y, new EmptyCell());
    //                 X = newX;
    //                 Y = newY;
    //                 map.SetCell(X, Y, this);
    //                 Energy--; // Reduz a energia em 1 após o movimento
    //                 
    //             }
    //             else
    //             {
    //                 throw new InvalidMoveException();
    //             }
    //         }
    //     }
    //
    //     public void PrintTotalJewels()
    //     {
    //         Console.WriteLine("Total de joias coletadas: " + Bag.Count);
    //         Console.WriteLine("Valor total das joias coletadas: " + Score);
    //     }
    //     
    //     public void InteractWithAdjacentItems()
    //     {
    //         CollectJewel();
    //         List<(int, int)> adjacentPositions = GetAdjacentPositions();
    //         foreach ((int adjX, int adjY) in adjacentPositions)
    //         {
    //             if (map.IsWithinBounds(adjX, adjY))
    //             {
    //                 ICell cell = map.GetCell(adjX, adjY);
    //                 if (cell is Jewel jewel)
    //                 {
    //                     map.SetCell(adjX, adjY, new EmptyCell());
    //                 }
    //                 else if (cell is Obstacle obstacle)
    //                 {
    //                     RechargeEnergy(obstacle);
    //                 }
    //             }
    //         }
    //     }
    //
    //     private List<(int, int)> GetAdjacentPositions()
    //     {
    //         List<(int, int)> positions = new List<(int, int)>();
    //         positions.Add((X - 1, Y)); // Cima
    //         positions.Add((X + 1, Y)); // Baixo
    //         positions.Add((X, Y - 1)); // Esquerda
    //         positions.Add((X, Y + 1)); // Direita
    //         return positions;
    //     }
    //     
    //     private void CollectJewel()
    //     {
    //         List<(int, int)> adjacentPositions = GetAdjacentPositions();
    //         foreach ((int adjX, int adjY) in adjacentPositions)
    //         {
    //             if (map.IsWithinBounds(adjX, adjY) && map.GetCell(adjX, adjY) is Jewel jewel)
    //             {
    //                 Score += jewel.Points;
    //                 Bag.Add(jewel);
    //
    //                 if (jewel.Symbol == "JB")
    //                 {
    //                     Energy += 5; // Adiciona 5 pontos de energia para a joia azul
    //                 }
    //
    //                 map.SetCell(adjX, adjY, new EmptyCell());
    //                 return;
    //             }
    //         }
    //     }
    //     
    //     private void RechargeEnergy(Obstacle obstacle)
    //     {
    //         Energy += obstacle.EnergyPoints;
    //     }
    // }
    // }
}