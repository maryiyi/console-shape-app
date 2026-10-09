using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConsoleApp1;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(Circle), typeDiscriminator: "circle")]
[JsonDerivedType(typeof(Rectangle), typeDiscriminator: "rectangle")]
[JsonDerivedType(typeof(Line), typeDiscriminator: "line")]
[JsonDerivedType(typeof(Triangle), typeDiscriminator: "triangle")]
public abstract class Shape
{
    public int Id { get; }
    public int X { get; set; }
    public int Y { get; set; }
    public ConsoleColor Color { get; set; }
    public FillMode FillMode { get;}
    
    public abstract int ExpectedParameterCount { get; }

    public Shape(int id, int x, int y, ConsoleColor color, FillMode fillMode)
    {
        Id = id;
        X = x;
        Y = y;
        Color = color;
        FillMode = fillMode;
    }
    public override string ToString()
    {
        return $"id: {Id} | {GetType().Name} | ({X}, {Y}) {Color} {FillMode}";
    }
    public virtual void Paint(int id, ConsoleColor newColor, Board board)
    {
            Shape shape = board.GetShapeById(id);
            if (shape != null)
            {
                shape.Color = newColor;
            }
        
    }
    public virtual void Edit(int[] newParam) { }
    public virtual bool IsBorder(int x, int y)
    {
        if (ContainsPoint(x, y))
        {
            return    !ContainsPoint(x, y + 1)
                   || !ContainsPoint(x, y - 1)
                   || !ContainsPoint(x + 1, y)
                   || !ContainsPoint(x - 1, y);
        }
        return false;
    }
    
    public bool Occupies(int x, int y)
    {
        return FillMode == FillMode.filled ? ContainsPoint(x, y) : IsBorder(x, y);
    }
    public void Draw(Pixel[,] grid, Board b)
    {
        char symbol = Color.ToString().ToLower()[0];

        for (int row = 0; row < b.GetHeight(); row++)
        {
            for (int col = 0; col < b.GetWidth(); col++)
            {
                if (Occupies(col, row))
                {
                    grid[row, col] = new Pixel(symbol, Color);
                }
            }
        }
    }
    public abstract bool ContainsPoint(int x, int y);

    
}