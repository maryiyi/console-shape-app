namespace ConsoleApp1;

public class Rectangle : Shape
{
    public int Height { get; set; }
    public int Width { get; set; }

    public Rectangle(int id, int x, int y, ConsoleColor color, FillMode fillMode, int height, int width)
        : base(id, x, y, color, fillMode)
    {
        Height = height;
        Width = width;
    }
    
    public override void Draw(Pixel[,] grid)
    {
        char symbol = Color.ToString().ToLower()[0];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (i >= 0 && i < Board.BOARD_HEIGHT && j >= 0 && j < Board.BOARD_WIDTH
                    && (FillMode == FillMode.filled || i == 0 || i == Height - 1 || j == 0 || j == Width - 1))
                {
                    grid[Y + i, X + j] = new Pixel(symbol, Color);
                }
            }
        }
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
    public override int GetBoarders()
    {
        return Math.Max(Height, Width);
    }

    public override bool ContainsPoint(int x, int y)
    {
        return (x >= X && x < X + Width && y >= Y && y < Y + Height);
    }
    
}
