using System;
using Jewel_Collector.Enums;

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
                        robot.Move(robot.X - 1, robot.Y);
                        break;
                    case 's':
                        robot.Move(robot.X + 1, robot.Y);
                        break;
                    case 'a':
                        robot.Move(robot.X, robot.Y - 1);
                        break;
                    case 'd':
                        robot.Move(robot.X, robot.Y + 1);
                        break;
                    case 'g':
                        robot.CollectJewel();
                        break;
                    default:
                        return;
                }
            }
        }
    }
}