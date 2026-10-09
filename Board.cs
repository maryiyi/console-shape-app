using System;
using System.Collections.Generic;
using ConsoleApp1;

public class Board
{
    private const int BOARD_WIDTH = 80;
    private const int BOARD_HEIGHT = 25;
    
    private List<Shape> _shapes = new List<Shape>();
    internal int? selectedShapeId = null;
    private int _nextId = 1;

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

        foreach (var shape in _shapes)
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
        foreach (var shape in _shapes)
        {
            Console.WriteLine($"{shape}");
        }
    }
    public IReadOnlyList<Shape> GetShapes()
    {
        return _shapes.AsReadOnly();
    }
    public void SetShapes(List<Shape> loadedShapes)
    {
        _shapes.AddRange(loadedShapes);
        if (_shapes.Any())
        {
            _nextId = _shapes.Max(s => s.Id) + 1;
        }
        else
        {
            _nextId = 1;
        }
    }
    public void Clear()
    {
        _shapes.Clear();
        Console.WriteLine("All cleared");
    }
    public void RemoveShape(int id)
    {
        var shape = GetShapeById(id);
        if (shape != null)
        {
            _shapes.Remove(shape);
        }
        else
        {
            Console.WriteLine($"no shape with this id {id}");
        }
    }
    public Shape GetShapeById(int id)
    {
        return _shapes.FirstOrDefault(s => s.Id == id);
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
        _shapes.Add(s);
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
    
    public int GetNextId()
    {
        return _nextId++;
    }
    
}