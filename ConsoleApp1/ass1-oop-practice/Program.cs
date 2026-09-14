using ConsoleApp1;

class Program
{
    // Визначення розмірів дошки
    
    // Структура для дошки
    
    static void Main(string[] args)
    {
        Board board = new Board();
        
        // Малювання трикутника у верхньому лівому кутку
        board.DrawTriangle(10, 1, 5);
        
        // Виведення дошки в консоль
        board.Print();
    }
}