using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1;

public class Triangle : Shape
{
    public int Height { get; set; }

    public Triangle(int id, int x, int y, ConsoleColor color, FillMode fillMode, int height) : base(id, x, y,
        color, fillMode)
    {
        Height = height;
    }
    

    public override void Draw(Pixel[,] grid)
    {
        char symbol = Color.ToString().ToLower()[0];
        for (int i = 0; i < Height; ++i) 
        {
            int numStarts = 2 * i + 1;
            int leftMost = X - i;
            for (int j = 0; j < numStarts; ++j)
            {
                int row = Y + i;
                int col = leftMost + j;

                if (row >= 0 && row < Board.BOARD_HEIGHT && col < Board.BOARD_WIDTH
                    && col >= 0 && (FillMode == FillMode.filled || IsBoarder(col, row)))
                {
                    grid[row, col] = new Pixel(symbol, Color);
                }
            }
        } 
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