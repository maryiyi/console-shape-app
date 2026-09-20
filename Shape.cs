using System;
using System.Collections.Generic;

namespace ConsoleApp1;

public abstract  class Shape
{
    public int Id { get; }
    public int X { get; set; }
    public int Y { get; set; }
    public ConsoleColor Color { get; set; }
    public FillMode FillMode { get; set; }

    public Shape(int id, int x, int y, ConsoleColor color, FillMode fillMode)
    {
        Id = id;
        X = x;
        Y = y;
        Color = color;
        FillMode = fillMode;
    }
    
    public abstract void Edit();
    
    // public abstract void Paint();  і так вже є  public ConsoleColor Color { get; set; }
    public abstract void Draw(Pixel[,] grid);
    public abstract bool ContainsPoint(int x, int y);
}