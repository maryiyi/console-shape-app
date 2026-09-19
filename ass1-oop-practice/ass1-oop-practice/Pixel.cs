namespace ConsoleApp1;

public class Pixel
{
    public char Symbol;
    public ConsoleColor Color;

    public Pixel(char symbol, ConsoleColor color = ConsoleColor.White)
    {
        Symbol = symbol;
        Color = color;
    }
    
}