using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConsoleApp1;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(Circle), typeDiscriminator: "circle")]
[JsonDerivedType(typeof(Rectangle), typeDiscriminator: "rectangle")]
[JsonDerivedType(typeof(Line), typeDiscriminator: "line")]
[JsonDerivedType(typeof(Triangle), typeDiscriminator: "triangle")]
public abstract partial class Shape
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

    public override string ToString()
    {
        return $"id: {Id} | {GetType().Name} | ({X}, {Y}) {Color} {FillMode}";
    }

    public virtual void Paint(int id, ConsoleColor newColor, Board board)
    {
        {
            Shape shape = board.GetShapeById(id);
            if (shape != null)
            {
                shape.Color = newColor;
            }
        }
    }

    public virtual void Edit(int[] newParam)
    {
    }
    

    // public abstract bool IsInside(int i, int j);

    // public abstract bool IsBorder(int i, int j);
    
    public abstract void Draw(Pixel[,] grid);
    public abstract bool ContainsPoint(int x, int y);
}