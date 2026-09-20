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

        public override void Draw(char[,] grid)
        {
            
        }
    }

}