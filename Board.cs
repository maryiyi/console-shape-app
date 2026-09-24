using System;
using System.Collections.Generic;
using ConsoleApp1;

public class Board
{
    public const int BOARD_WIDTH = 80;
    public const int BOARD_HEIGHT = 25;


    public List<Shape> shapes = new List<Shape>();

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
            shape.Draw(grid);
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

    public void ListShapes()
    {
        foreach (var shape in shapes)
        {
            Console.WriteLine($"{shape}");
        }
    }

    public List<Shape> GetShapes()
    {
        return shapes;
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
            Console.WriteLine($"No shape with this id {id}");
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
            Console.WriteLine("No such shape");
        }
    }

    public void AddShape(Shape s)
    {
        shapes.Add(s);
    }
    
}