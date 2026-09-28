using System;
using System.Collections.Generic;
using ConsoleApp1;

public class Board
{
    private const int BOARD_WIDTH = 80;
    private const int BOARD_HEIGHT = 25;
    
    private List<Shape> shapes = new List<Shape>();
    int? selectedShapeId = null;

    public void Draw()
    {
        Pixel[,] grid = new Pixel[BOARD_HEIGHT, BOARD_WIDTH];

        for (int i = 0; i < BOARD_HEIGHT; i++)
        {
            for (int j = 0; j < BOARD_WIDTH; j++)
            {
                grid[i, j] = new Pixel(' ');
            }
        }

        foreach (var shape in shapes)
        {
            shape.Draw(grid, this);
        }

        for (int i = 0; i < BOARD_HEIGHT; i++)
        {
            for (int j = 0; j < BOARD_WIDTH; j++)
            {
                Console.ForegroundColor = grid[i, j].Color;
                Console.Write(grid[i, j].Symbol);
            }

            Console.WriteLine();
        }

        Console.ResetColor();

    }

    public int GetHeight() { return BOARD_HEIGHT;}
    public int GetWidth() { return BOARD_WIDTH;}
    public void ListShapes()
    {
        foreach (var shape in shapes)
        {
            Console.WriteLine($"{shape}");
        }
    }
    public IReadOnlyList<Shape> GetShapes()
    {
        return shapes.AsReadOnly();
    }
    public void SetShape(List<Shape> loadedShapes)
    {
        shapes = loadedShapes;
    }
    public void Clear()
    {
        shapes.Clear();
        Console.WriteLine("All cleared");
    }
    public void RemoveShape(int id)
    {
        var shape = GetShapeById(id);
        if (shape != null)
        {
            shapes.Remove(shape);
        }
        else
        {
            Console.WriteLine($"no shape with this id {id}");
        }
    }
    public Shape GetShapeById(int id)
    {
        return shapes.FirstOrDefault(s => s.Id == id);
    }
    public void MoveShape(int id, int newX, int newY)
    {
        var shape = GetShapeById(id);
        if (shape != null)
        {
            shape.X = newX;
            shape.Y = newY;
            Console.WriteLine($"shape with new coordinates: {newX}, {newY}");
        }
        else
        {
            Console.WriteLine("no such shape");
        }
    }
    public void AddShape(Shape s)
    {
        shapes.Add(s);
    }
    public void PrintAvailableShapes()
    {
        Console.WriteLine("available shapes to add:\n" +
                          "circle: [id] [x] [y] [color] [fillMode] [radius]\n" +
                          "rectangle: [id] [x] [y] [color] [fillMode] [height] [width]\n" +
                          "line: [id] [x] [y] [color] [fillMode] [length]\n" +
                          "triangle: [id] [x] [y] [color] [fillMode] [height]\n");
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
                    AddShape(circle);
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
                        AddShape(rectangle);
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
                        AddShape(line);
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
                        AddShape(triangle);
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
            RemoveShape(outputId);
        }
        else
        {
            Console.WriteLine("write an id of the shape you want to remove!");
        }
    }

    public void ClearSelectedId()
    {
        selectedShapeId = null;
    }
    public void SelectCmd(string[] parts)
    {
        if (parts.Length == 2 && int.TryParse(parts[1], out int outputId))
        {
            var shape = GetShapeById(outputId);
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
            var shape = GetShapes().LastOrDefault(shape => shape.ContainsPoint(coX, coY));
            if (shape != null)
            {
                selectedShapeId = shape.Id;
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
            MoveShape(outputId, coX, coY);  
        }
        else
        {
            Console.WriteLine("wrong parameters. ex: move [id] [newX] [newY]");
        }
    }

    public void PaintCms(string[] parts)
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
            Shape shape = GetShapeById(selectedShapeId.Value);
            shape.Paint(selectedShapeId.Value, newColor, this);
        }
        else
        {
            Console.WriteLine("error: no shape selected");
        }
    }

    public void EditCmd(string[] parts)
    {
        if (selectedShapeId != null && parts.Length >= 2)
        {
            Shape shape = GetShapeById(selectedShapeId.Value);
            List<int> tempList = new List<int>();
            for (int i = 1; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out int num))
                {
                    tempList.Add(num);
                }
            }
            int[] param = tempList.ToArray();
            if (param.Length > 0)
            {
                shape.Edit(param);
                Console.WriteLine("shape updated");
            }
        }
        else
        {
            Console.WriteLine("not selected any shape or too much parametrs");
        }
    }
}