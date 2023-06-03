using System;
using System.Collections.Generic;
using Jewel_Collector.Enums;
using Jewel_Collector.Exceptions;

namespace Jewel_Collector
{
    public class JewelCollector
    {
        public static void Main()
        {
            Map map = new Map(10);

            map.SetCell(1, 9, new Jewel(JewelType.Red));
            map.SetCell(8, 8, new Jewel(JewelType.Red));
            map.SetCell(9, 1, new Jewel(JewelType.Green));
            map.SetCell(7, 6, new Jewel(JewelType.Green));
            map.SetCell(3, 4, new Jewel(JewelType.Blue));
            map.SetCell(2, 1, new Jewel(JewelType.Blue));

            map.SetCell(5, 0, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 1, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 2, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 3, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 4, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 5, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 6, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 9, new Obstacle(ObstacleType.Tree));
            map.SetCell(3, 9, new Obstacle(ObstacleType.Tree));
            map.SetCell(8, 3, new Obstacle(ObstacleType.Tree));
            map.SetCell(2, 5, new Obstacle(ObstacleType.Tree));
            map.SetCell(1, 4, new Obstacle(ObstacleType.Tree));

            Robot robot = new Robot(0, 0, map);

            while (true)
            {
                map.PrintMap(robot);
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'w':
                        HandleRobotMovement(robot, robot.X - 1, robot.Y);
                        break;
                    case 's':
                        HandleRobotMovement(robot, robot.X + 1, robot.Y);
                        break;
                    case 'a':
                        HandleRobotMovement(robot, robot.X, robot.Y - 1);
                        break;
                    case 'd':
                        HandleRobotMovement(robot, robot.X, robot.Y + 1);
                        break;
                    case 'g':
                        robot.InteractWithAdjacentItems();
                        if (map.GetTotalJewels() == 0)
                        {
                            map.IncreaseSize();
                            map.IncrementPhase();
                            map.RandomizeItems();

                            int robotX = robot.X;
                            int robotY = robot.Y;
                            
                            // Procurar a próxima célula vazia adjacente
                            List<(int, int)> adjacentPositions = robot.GetAdjacentPositions();
                            foreach ((int adjX, int adjY) in adjacentPositions)
                            {
                                if (map.IsWithinBounds(adjX, adjY) && map.GetCell(adjX, adjY) is EmptyCell)
                                {
                                    robotX = adjX;
                                    robotY = adjY;
                                    break;
                                }
                            }
                            
                            robot.Move(robotX, robotY);
                        }

                        break;
                    default:
                        return;
                }
            }
        }

        private static void HandleRobotMovement(Robot robot, int newX, int newY)
        {
            try
            {
                robot.Move(newX, newY);

                Map map = robot.GetMap();
                ICell destinationCell = map.GetCell(newX, newY);
                int fase = map.GetPhase();

                List<(int, int)> adjacentPositions = robot.GetAdjacentPositions();
                foreach ((int adjX, int adjY) in adjacentPositions)
                {
                    if (map.IsWithinBounds(adjX, adjY))
                    {
                        ICell cell = map.GetCell(adjX, adjY);
                        if (cell is Radioactive)
                        {
                            robot.LoseEnergy(10);
                        }
                    }
                }

                if (fase >= 2 && destinationCell is Radioactive)
                {
                    Console.WriteLine(
                        "Você está próximo a um elemento radioativo. Deseja transpô-lo? (Digite 'g' para transpor)");
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.KeyChar == 'g')
                    {
                        robot.TransposeRadioactive(newX, newY);
                    }
                }
            }
            catch (OutOfMapBoundsException)
            {
                Console.WriteLine("ERRO: A posição está fora dos limites do mapa.");
                throw;
            }
            catch (InvalidMoveException)
            {
                Console.WriteLine("ERRO: Movimento inválido. A posição está ocupada por outro item.");
                throw;
            }
        }
    }
}