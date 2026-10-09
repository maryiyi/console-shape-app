namespace ConsoleApp1;

public class Rectangle : Shape
{
    public int Height { get; set; }
    public int Width { get; set; }
    public override int ExpectedParameterCount => 2;

    public Rectangle(int id, int x, int y, ConsoleColor color, FillMode fillMode, int height, int width)
        : base(id, x, y, color, fillMode)
    {
        Height = height;
        Width = width;
    }

    public override string ToString()
    {
        return base.ToString() + $"height: {Height} width: {Width}";
    }
    public override void Edit(int[] newParam)
    {
        Height = newParam[0];
        Width = newParam[1];
    }
    public override bool ContainsPoint(int x, int y)
    {
        return (x >= X && x < X + Width && y >= Y && y < Y + Height);
    }
    
}
