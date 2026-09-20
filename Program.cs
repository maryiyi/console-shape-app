using ConsoleApp1;

Board b = new Board();

Triangle t = new Triangle(1, 20, 2, ConsoleColor.Yellow, FillMode.filled, 5);
Line l = new Line(2, 20, 10, ConsoleColor.Cyan, FillMode.filled, 5, Direction.Up);
Rectangle r = new Rectangle(3, 15, 1, ConsoleColor.Red, FillMode.filled, 4, 6);

b.AddShape(t);
b.AddShape(l);
b.AddShape(r);

b.Draw();
