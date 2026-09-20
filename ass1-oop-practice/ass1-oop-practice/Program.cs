using ConsoleApp1;

Board b = new Board();
Triangle t = new Triangle(1, 20, 2, ConsoleColor.Yellow, FillMode.filled, 5);
Line l = new Line(2, 2, 3, ConsoleColor.Cyan, FillMode.filled, 5, Direction.Up);
    
b.AddShape(t);
b.AddShape(l);

b.Draw();
