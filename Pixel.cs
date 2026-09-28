namespace ConsoleApp1;

public readonly struct Pixel
{
     public readonly char Symbol;
     public readonly ConsoleColor Color;

    public Pixel(char symbol, ConsoleColor color = ConsoleColor.White)
    {
        Symbol = symbol;
        Color = color;
    }
    
}