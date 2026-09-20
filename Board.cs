using System;
using System.Collections.Generic;
using ConsoleApp1;

class Board
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

    // public void Print()
    // {
    //     foreach (var row in grid)
    //     {
    //         foreach (char c in row)
    //         {
    //             Console.Write(c);
    //         }
    //         Console.WriteLine();
    //     }
    // }
    
    public void AddShape(Shape s)
    {
        shapes.Add(s);
    }

    public void RemoveShape(Shape s)
    {
        shapes.Remove(s);
    }
}