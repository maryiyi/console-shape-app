
namespace ConsoleApp1
{
    public class Circle : Shape
    {
        public int Radius { get; set; }
        public override int ExpectedParameterCount => 1;

        public Circle(int id, int x, int y, ConsoleColor color, FillMode fillMode, int radius) : base(id, x, y,
            color, fillMode)
        {
            Radius = radius;
        }

        public override string ToString()
        {
            return base.ToString() + $"radius: {Radius}";
        }
        public override void Edit(int[] newParam)
        {
            Radius = newParam[0];
        }

    
        
        public override bool ContainsPoint(int x, int y)
        {
            int dx = X - x;
            int dy = Y - y;

            return ((dx * dx + dy * dy) <= Radius * Radius);
        }
    }

}