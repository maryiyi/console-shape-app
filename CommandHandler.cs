namespace ConsoleApp1;

public class CommandHandler
{
    private readonly Board _board;

    public CommandHandler(Board board)
    {
        _board = board;
    }
        
    public void AddShapeCmd(string output)
    {
        string[] parts = output.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 6)
        {
            Console.WriteLine("not enough parametrs!");
            return;
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
                return;
            }

            int id = _board.GetNextId();
            
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
                    _board.AddShape(circle);
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
                        Console.WriteLine("height and width have to be numbers!");
                        break;
                    }
                    {
                        Rectangle rectangle = new Rectangle(id, x, y, color, fillMode, heightR, width);
                        _board.AddShape(rectangle);
                        Console.WriteLine($"rectangle was added, id: {id}");
                    }
                    break;
                
                case "line":
                    if (parts.Length < 8)
                    {
                        Console.WriteLine("not enough parameters for line ! need: x, y, length, direction");
                        break;
                    }
                    if (!int.TryParse(parts[6], out int length))
                    {
                        Console.WriteLine("length has to be a number!");
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
                        _board.AddShape(line);
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
                        _board.AddShape(triangle);
                        Console.WriteLine($"triangle was added, id: {id}");
                    }
                    break;
                default:
                    Console.WriteLine($"unknown shape type {strType}");
                    break;

            }
        
    }

    public void RemoveCmd(string[] parts)
    {
        if (parts.Length > 1 && int.TryParse(parts[1], out int outputId))
        {
            _board.RemoveShape(outputId);
        }
        else
        {
            Console.WriteLine("write an id of the shape you want to remove!");
        }
    }

    
    public void SelectCmd(string[] parts)
    {
        if (parts.Length == 2 && int.TryParse(parts[1], out int outputId))
        {
            var shape = _board.GetShapeById(outputId);
            if (shape != null)
            {
                _board.selectedShapeId = outputId;
                Console.WriteLine($"selected shape: {shape}");
            }
            else
            {
                Console.WriteLine($"no such shape");
            }
        }
        else if(parts.Length >= 3 && int.TryParse(parts[1], out int coX) && int.TryParse(parts[2], out int coY))
        {
            var shape = _board.GetShapes().LastOrDefault(shape => shape.Occupies(coX, coY));
            if (shape != null)
            {
                _board.selectedShapeId = shape.Id;
                Console.WriteLine($"selected shape by coordinates: {shape}");
            }
            else
            {
                Console.WriteLine($"no such shape");
            }
        }
        else
        {
            Console.WriteLine("wrong parametrs!");
        }
    }

    public void MoveCmd(string[] parts)
    {
        if (parts.Length == 4 && int.TryParse(parts[1], out int outputId) 
                              && int.TryParse(parts[2], out int coX) 
                              && int.TryParse(parts[3], out int coY))
        {
            _board.MoveShape(outputId, coX, coY);  
        }
        else
        {
            Console.WriteLine("wrong parameters. ex: move [id] [newX] [newY]");
        }
    }

    public void PaintCms(string[] parts)
    {
        if (_board.selectedShapeId != null && parts.Length >= 2)
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
            Shape shape = _board.GetShapeById(_board.selectedShapeId.Value);
            if (_board.selectedShapeId != null)
            {
                shape.Paint(_board.selectedShapeId.Value, newColor, _board);
            }
            else
            {
                Console.WriteLine("error: selected shape does not exist");
                _board.selectedShapeId = null;
            }
        }
        else
        {
            Console.WriteLine("error: no shape selected");
        }
    }

    public void EditCmd(string[] parts)
    {
        if (_board.selectedShapeId == null)
        {
            Console.WriteLine("no shape selected");
            return;
        }
        Shape shape = _board.GetShapeById(_board.selectedShapeId.Value);
        if (shape == null)
        {
            Console.WriteLine("selected shape not found");
            return;
        }
        

        List<int> tempList = new List<int>();
        for (int i = 1; i < parts.Length; i++)
        {
            if (int.TryParse(parts[i], out int num))
            {
                tempList.Add(num);
            }
            else
            {
                Console.WriteLine("all parametrs must be numbers");
                return;
            }
        }
        
        int[] param = tempList.ToArray();
        if (param.Length != shape.ExpectedParameterCount)
        {
            Console.WriteLine(
                $"error! shape {shape.GetType().Name} are expected to have {shape.ExpectedParameterCount} parametrs");
            return;
        }

        shape.Edit(param);
        Console.WriteLine("shape updated");
    }
}