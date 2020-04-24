using Codecool.Quest.Models.Actors;
using Codecool.Quest.Models.ItemsCollection;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Codecool.Quest.Models
{
    public class MapLoader
    {
        public static GameMap LoadMap()
        {
            using var stream = new StreamReader("map.txt");
            var firstLine = stream.ReadLine();
            var firstLineSplit = firstLine.Split(' ');

            var width = int.Parse(firstLineSplit[0]);
            var height = int.Parse(firstLineSplit[1]);

            var map = new GameMap(width, height, CellType.Empty);

            for (var y = 0; y < height; y++)
            {
                var line = stream.ReadLine();

                for (var x = 0; x < width; x++)
                {
                    if (x < line.Length)
                    {
                        var cell = map.GetCell(x, y);

                        switch (line[x])
                        {
                            case ' ':
                            {
                                cell.CellType = CellType.Empty;
                                break;
                            }
                            case '#':
                            {
                                cell.CellType = CellType.Wall;
                                break;
                            }
                            case '.':
                            {
                                cell.CellType = CellType.Floor;
                                break;
                            }
                            case 's':
                            {
                                cell.CellType = CellType.Floor;
                                map.Skeleton = new Skeleton(cell);
                                break;
                            }
                            case '@':
                            {
                                cell.CellType = CellType.Floor;
                                map.Player = new Player(cell);
                                break;
                            }
                            case 'k':
                            {
                                cell.CellType = CellType.Floor;
                                map.Key = new Key(cell);
                                break;
                            }
                            case 'w':
                            {
                                cell.CellType = CellType.Floor;
                                map.Sword = new Sword(cell);
                                break;
                            }

                            case 'a':
                            {
                                cell.CellType = CellType.CornerLeft;
                                break;

                            }
                            case 'b':
                            {
                                cell.CellType = CellType.UpperFrame;
                                break;

                            }
                            case 'c':
                            {
                                cell.CellType = CellType.CornerRight;
                                break;

                            }
                            case 'd':
                            {
                                cell.CellType = CellType.RightSide;
                                break;

                            }
                            case 'h':
                            {
                                cell.CellType = CellType.LeftSide;
                                break;

                            }
                            case 'e':
                            {
                                cell.CellType = CellType.LeftDownCorn;
                                break;

                            }
                            case 'g':
                            {
                                cell.CellType = CellType.DownRightCorn;
                                break;

                            }
                            case 'f':
                            {
                                cell.CellType = CellType.DownFrame;
                                break;

                            }
                            case 'i':
                            {
                                cell.CellType = CellType.Floor;
                                map.Door = new Door(cell);
                                break;

                            }

                        }
                    }
                }
            }

            return map;
        }
    }
}