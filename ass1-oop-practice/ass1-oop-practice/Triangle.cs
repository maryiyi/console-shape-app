namespace ConsoleApp1;

public class Triangle : Shape
{
    public int Height { get; set; }

    public Triangle(int id, int x, int y, ConsoleColor color, FillMode fillMode, int height) : base(id, x, y,
        color, fillMode)
    {
        Height = height;
    }

    public override void Edit()
    {
        throw new NotImplementedException();
    }

    public override void Draw(Pixel[,] grid)
    {
        char symbol = Color.ToString().ToLower()[0];
        for (int i = 0; i < Height; ++i) {
            int numStars = 2 * i + 1;
            int leftMost = X - i;
            for (int j = 0; j < numStars; ++j) {
                int position = leftMost + j;
                if (position >= 0 && position < Board.BOARD_WIDTH && (Y + i) <
                    Board.BOARD_HEIGHT && (Y + i) >= 0)
                    grid[Y + i, position] = new Pixel(symbol, Color);
            }
        } 
    }

    public override bool ContainsPoint(int x, int y)
    {
        throw new NotImplementedException();
    }
}