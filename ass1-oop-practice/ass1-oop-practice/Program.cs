using ConsoleApp1;

Board b = new Board();

Triangle t = new Triangle(1, 20, 2, ConsoleColor.Magenta, FillMode.filled, 5);
b.AddShape(t);
b.Draw();
