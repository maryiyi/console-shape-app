using System.Data;

namespace ConsoleApp1;

public class Line : Shape
{
    public int Length { get; set; }
    public Direction Direction { get; set; }

    public Line(int id, int x, int y, ConsoleColor color, FillMode fillMode, int length, Direction direction) 
        : base(id, x, y, color, fillMode)
    {
        Length = length;
        Direction = direction;
    }


    

    public override void Draw(Pixel[,] grid)
    {
        char symbol = Color.ToString().ToLower()[0];
        for (int i = 0; i < Length; i++)
        {
            int row = Y, col = X;
            if (Direction == Direction.Up)
            {
                row = Y - i;
            }
            else if (Direction == Direction.Down)
            {
                row = Y + i;
            }
            else if (Direction == Direction.Left)
            {
                col = X - i;
            }
            else if (Direction == Direction.Right)
            {
                col = X + i;
            }
            else
            {
                throw new ArgumentOutOfRangeException("False direction! Please choose: Up, Down, Left or Right");
            }

            if (row >= 0 && row < Board.BOARD_HEIGHT && col >= 0 && col < Board.BOARD_WIDTH)
            {
                grid[row, col] = new Pixel(symbol, Color);
            }

        }
    }

    public override string ToString()
    {
        return base.ToString() + $"length: {Length}";
    }
    
    public override void Edit(int[] newParam)
    {
        Length = newParam[0];
    }

    public override bool ContainsPoint(int x, int y)
    {
        throw new NotImplementedException();
    }
    

}