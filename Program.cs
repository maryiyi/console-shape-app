using System.Globalization;
using ConsoleApp1;

Board b = new Board();
//
// Triangle t = new Triangle(1, 20, 2, ConsoleColor.Yellow, FillMode.filled, 5);
// Line l = new Line(2, 20, 10, ConsoleColor.Cyan, FillMode.filled, 5, Direction.Up);
// Rectangle r = new Rectangle(3, 15, 1, ConsoleColor.Red, FillMode.filled, 4, 6);
// Circle c = new Circle(4, 10, 2, ConsoleColor.Blue, FillMode.filled, 3);
//
//
// b.AddShape(t);
// b.AddShape(l);
// b.AddShape(r);
// b.AddShape(c);

while (true)
{
    Console.WriteLine("write a command:\ndraw list shapes add select remove edit paint move clear save load");
    string output = Console.ReadLine();
    string[] parts = output.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string cmd = parts[0].ToLower();

    switch (cmd)
    {
        case "draw":
            b.Draw();
            break;
        case "list":
            b.ListShapes();
            break;
        case "shapes":
            Console.WriteLine("Available shapes to add:\n" +
                              "Circle: [id] [x] [y] [color] [fillMode] [radius]\n" +
                              "Rectangle: [id] [x] [y] [color] [fillMode] [height] [width]\n" +
                              "Line: [id] [x] [y] [color] [fillMode] [length]\n" +
                              "Triangle: [id] [x] [y] [color] [fillMode] [height]\n"); break;
        case "add":
            if (parts.Length < 5)
            {
                Console.WriteLine("not enough parametrs!");
                break;
            }

            string strFillMode = parts[1].ToLower();
            string strColor = parts[2].ToLower();
            string strType = parts[3].ToLower();

            FillMode fillMode = (strFillMode == "filled") ? FillMode.filled : FillMode.frame;
            ConsoleColor color = strColor switch
            {
                "red" => ConsoleColor.Red,
                "blue" => ConsoleColor.Blue,
                "green" => ConsoleColor.Green,
                "yellow" => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
            int id = new Random().Next(1, 100);
            switch (strType)
            {
                case "circle":
                    if (parts.Length < 7)
                    {
                        Console.WriteLine("not enough parameters for circle! need: x, y, radius");
                        break;
                    }

                {
                    int x = int.Parse(parts[4]);
                    int y = int.Parse(parts[5]);
                    int radius = int.Parse(parts[6]);

                    Circle circle = new Circle(id, x, y, color, fillMode, radius);
                    b.AddShape(circle);
                    Console.WriteLine($"circle was added, id: {id}");
                }
                    break;
                case "rectangle":
                    if (parts.Length < 8)
                    {
                        Console.WriteLine("not enough parameters for rectangle! need: x, y, height, width");
                        break;
                    }

                {
                    int x = int.Parse(parts[4]);
                    int y = int.Parse(parts[5]);
                    int height = int.Parse(parts[6]);
                    int width = int.Parse(parts[7]);

                    Rectangle rectangle = new Rectangle(id, x, y, color, fillMode, height, width);
                    b.AddShape(rectangle);
                    Console.WriteLine($"rectangle was added, id: {id}");
                }
                    break;
                case "line":
                    if (parts.Length < 8)
                    {
                        Console.WriteLine("not enough parameters for line! need: x, y, length, direction");
                        break;
                    }

                {
                    int x = int.Parse(parts[4]);
                    int y = int.Parse(parts[5]);
                    int length = int.Parse(parts[6]);
                    string dirStr = parts[7].ToLower();
                    Direction direction = dirStr switch
                    {
                        "up" => Direction.Up,
                        "down" => Direction.Down,
                        "left" => Direction.Left,
                        "right" => Direction.Right,
                        _ => Direction.Left
                    };

                    Line line = new Line(id, x, y, color, fillMode, length, direction);
                    b.AddShape(line);
                    Console.WriteLine($"line was added, id: {id}");
                }
                    break;
                case "triangle":
                    if (parts.Length < 7)
                    {
                        Console.WriteLine("not enough parameters for circle! need: x, y, height");
                        break;
                    }

                {
                    int x = int.Parse(parts[4]);
                    int y = int.Parse(parts[5]);
                    int height = int.Parse(parts[6]);

                    Triangle triangle = new Triangle(id, x, y, color, fillMode, height);
                    b.AddShape(triangle);
                    Console.WriteLine($"triangle was added, id: {id}");
                }
                    break;
                default:
                    Console.WriteLine($"Unknown shape type {strType}");
                    break;

            }

            break;
        case "save":
        {
            string filename = parts.Length > 1 ? parts[1] : "shapes.json";
            FileManager fileManager = new FileManager();
            fileManager.SaveToFile(filename, b);
        }
            break;
        case "load":
        {
            string filename = parts.Length > 1 ? parts[1] : "shapes.json";
            FileManager fileManager = new FileManager();
            fileManager.LoadFromFile(filename, b);
        }
            break;
        case "clear":
        {
            b.Clear();
            break;
        }
        case "remove":
        {
            if (parts.Length > 1 && int.TryParse(parts[1], out int outputId))
            {
                b.RemoveShape(outputId);
            }
            else
            {
                Console.WriteLine("Write an id of the shape you want to remove!");
            }

            break;
        }
        case "select":
        {
            if (parts.Length == 2 && int.TryParse(parts[1], out int outputId))
            {
                var selectedShape = b.GetShapeById(outputId);
                if (selectedShape != null)
                {
                    Console.WriteLine($"Selected shape: {selectedShape}");
                }
                else
                {
                    Console.WriteLine($"No such shape");
                }
            }
            else if(parts.Length >= 3 && int.TryParse(parts[1], out int coX) && int.TryParse(parts[2], out int coY))
            {
                var shape = b.GetShapes().LastOrDefault(shape => shape.ContainsPoint(coX, coY));
                if (shape != null)
                {
                    Console.WriteLine($"Selected shape by coordinates: {shape}");
                }
                else
                {
                    Console.WriteLine($"No such shape");
                }
            }
            else
            {
                Console.WriteLine("Wrong parametrs!");
            }
            break;
        }
        case "move":
        {
            if (parts.Length == 4 && int.TryParse(parts[1], out int outputId) 
                                  && int.TryParse(parts[2], out int coX) 
                                  && int.TryParse(parts[3], out int coY))
            {
              b.MoveShape(outputId, coX, coY);  
            }
            else
            {
                Console.WriteLine("wrong parameters. ex: move [id] [newX] [newY]");
            }
            break;
        }
        

    default:
            Console.WriteLine("Uncorrect command!");
            break;
    }
}


