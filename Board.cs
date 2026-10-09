using System;
using System.Collections.Generic;
using ConsoleApp1;

public class Board
{
    private const int BOARD_WIDTH = 80;
    private const int BOARD_HEIGHT = 25;
    
    private List<Shape> shapes = new List<Shape>();
    internal int? selectedShapeId = null;

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
    public void ClearSelectedId()
    {
        selectedShapeId = null;
    }
    

    
}