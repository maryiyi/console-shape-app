using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1;

public class Triangle : Shape
{
    public int Height { get; set; }
    public override int ExpectedParameterCount => 1;

    public Triangle(int id, int x, int y, ConsoleColor color, FillMode fillMode, int height) : base(id, x, y,
        color, fillMode)
    {
        Height = height;
    }
    public override string ToString()
    {
        return base.ToString() + $" height: {Height}";
    }
    public override void Edit(int[] newParam)
    { 
        Height = newParam[0];
    }
    public override bool ContainsPoint(int x, int y)
    {
        int dy = y - Y;
        
        if (dy >= 0 && dy < Height)
        {
            int dx = x - X;
            return dx >= -dy && dx <= dy;
        }
        
        return false;
            
    }
    
}