using SFML.Graphics;
using Color = System.Drawing.Color;

namespace ConsoleApp1
{
    public class Circle : Shape
    {
        public int Radius { get; set; }

        public Circle(int id, int x, int y, ConsoleColor color, FillMode fillMode, int radius) : base(id, x, y,
            color, fillMode)
        {
            Radius = radius;
        }

        public override void Draw(Pixel[,] grid)
        {
            char symbol = Color.ToString().ToLower()[0];
            for (int i = -Radius; i <= Radius; i++)
            {
                for (int j = -Radius; j <= Radius; j++)
                {
                    int row = Y + i;
                    int col = X + j;
                    
                    if (row >= 0 && row < Board.BOARD_HEIGHT && col >= 0 && col < Board.BOARD_WIDTH && 
                        ((FillMode == FillMode.filled && ContainsPoint(col, row)) 
                            || (FillMode == FillMode.frame && IsBoarder(col, row)))) 
                    { 
                        grid[row, col] = new Pixel(symbol, Color);
                    }   
                }
            }
        }

        public override string ToString()
        {
            return base.ToString() + $"radius: {Radius}";
        }
        public override void Edit(int[] newParam)
        {
            Radius = newParam[0];
        }

        public override int GetBoarders()
        {
            return Radius;
        }
    
        
        public override bool ContainsPoint(int x, int y)
        {
            int dx = X - x;
            int dy = Y - y;

            return ((dx * dx + dy * dy) <= Radius * Radius);
        }
    }

}