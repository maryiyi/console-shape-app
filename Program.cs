using System.Globalization;
using System.Linq;
using ConsoleApp1;

Board b = new Board();
int? selectedShapeId = null;

while (true)
{
    Console.WriteLine("write a command:\ndraw list shapes add select remove edit paint move clear save load");
    string output = Console.ReadLine();
    string[] parts = output.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    
    if (parts.Length == 0){continue;} 
    
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
            Console.WriteLine("available shapes to add:\n" +
                              "circle: [id] [x] [y] [color] [fillMode] [radius]\n" +
                              "rectangle: [id] [x] [y] [color] [fillMode] [height] [width]\n" +
                              "line: [id] [x] [y] [color] [fillMode] [length]\n" +
                              "triangle: [id] [x] [y] [color] [fillMode] [height]\n"); break;
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

            if (!int.TryParse(parts[4], out int x) || !int.TryParse(parts[5], out int y))
            {
                Console.WriteLine("error! x and y must be numbers");
                break;
            }

            int id = new Random().Next(1, 100);

            switch (strType)
            {
                case "circle":
                    if (parts.Length < 7)
                    {
                        Console.WriteLine("not enough parameters for circle ! need: x, y, radius");
                        break;
                    }

                    if (!int.TryParse(parts[6], out int radius))
                    {
                        Console.WriteLine("radius must be a number");
                        break;
                    }

                {
                    Circle circle = new Circle(id, x, y, color, fillMode, radius);
                    b.AddShape(circle);
                    Console.WriteLine($"circle was added, id: {id}");
                }
                    break;
                case "rectangle":
                    if (parts.Length < 8)
                    {
                        Console.WriteLine(
                            "not enough parameters for rectangle! need: x, y, height, width");
                        break;
                    }

                    if (!int.TryParse(parts[6], out int heightR) || !int.TryParse(parts[7], out int width))
                    {
                        Console.WriteLine("not enough parameters for rectangle!");
                        break;
                    }
                    {
                        Rectangle rectangle = new Rectangle(id, x, y, color, fillMode, heightR, width);
                        b.AddShape(rectangle);
                        Console.WriteLine($"rectangle was added, id: {id}");
                    }
                    break;
                
                case "line":
                    if (parts.Length < 8 || !int.TryParse(parts[6], out int length))
                    {
                        Console.WriteLine("not enough parameters for line or lenght is not a number! need: x, y, length, direction");
                        break;
                    }

                    {
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
                        Console.WriteLine("not enough parameters for triangle! need: x, y, height");
                        break;
                    }

                    if (!int.TryParse(parts[6], out int height))
                    {
                        Console.WriteLine("height is not a number!");
                        break;
                    }
                    {
                        Triangle triangle = new Triangle(id, x, y, color, fillMode, height);
                        b.AddShape(triangle);
                        Console.WriteLine($"triangle was added, id: {id}");
                    }
                    break;
                default:
                    Console.WriteLine($"unknown shape type {strType}");
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
            selectedShapeId = null;
        }
            break;
        case "clear":
        {
            b.Clear();
            selectedShapeId = null;
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
                Console.WriteLine("write an id of the shape you want to remove!");
            }

            break;
        }
        case "select":
        {
            if (parts.Length == 2 && int.TryParse(parts[1], out int outputId))
            {
                var shape = b.GetShapeById(outputId);
                if (shape != null)
                {
                    selectedShapeId = outputId;
                    Console.WriteLine($"selected shape: {shape}");
                }
                else
                {
                    Console.WriteLine($"no such shape");
                }
            }
            else if(parts.Length >= 3 && int.TryParse(parts[1], out int coX) && int.TryParse(parts[2], out int coY))
            {
                var shape = b.GetShapes().LastOrDefault(shape => shape.ContainsPoint(coX, coY));
                if (shape != null)
                {
                    selectedShapeId = shape.Id;
                    Console.WriteLine($"Selected shape by coordinates: {shape}");
                }
                else
                {
                    Console.WriteLine($"No such shape");
                }
            }
            else
            {
                Console.WriteLine("wrong parametrs!");
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
        case "paint":
        {
            if (selectedShapeId != null & parts.Length >= 2)
            {
                string colorStr = parts[1].ToLower();
                ConsoleColor newColor = colorStr switch
                {
                    "red" => ConsoleColor.Red,
                    "blue" => ConsoleColor.Blue,
                    "green" => ConsoleColor.Green,
                    "yellow" => ConsoleColor.Yellow,
                    _ => ConsoleColor.White
                };
                Shape shape = b.GetShapeById(selectedShapeId.Value);
                shape.Paint(selectedShapeId.Value, newColor, b);
            }
            else
            {
                Console.WriteLine("error: no shape selected");
            }
            break;
        }
        case "edit":
        {
            if (selectedShapeId != null && parts.Length >= 2)
            {
                Shape shape = b.GetShapeById(selectedShapeId.Value);

                if (shape is Triangle triangle && int.TryParse(parts[1], out int newHeight))
                {
                    triangle.Height = newHeight;
                    Console.WriteLine("triangle updated!");
                }
                else if (shape is Circle circle && int.TryParse(parts[1], out int newRadius))
                {
                    circle.Radius = newRadius;
                    Console.WriteLine("circle updated!");
                }
                else if (shape is Rectangle rectangle && int.TryParse(parts[1], out int newHeightRec) && int.TryParse(parts[2], out int newWidthRec))
                {
                    rectangle.Height = newHeightRec;
                    rectangle.Width = newWidthRec;
                    Console.WriteLine("rectangle updated!");
                }
                else if (shape is Line line && int.TryParse(parts[1], out int newLength))
                {
                    line.Length = newLength;
                    Console.WriteLine("line updated!");
                }
            }
            else
            {
                Console.WriteLine("not selected any shape or too much parametrs");
            }

            break;
        }
        
    default:
            Console.WriteLine("uncorrect command!");
            break;
    }
}


