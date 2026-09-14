namespace ConsoleApp1;
using System;
using System.Collections.Generic;

struct Board
{
    const int BOARD_WIDTH = 80;
    const int BOARD_HEIGHT = 25;

    public List<List<char>> grid;
    
    public Board()
    {
        grid = new List<List<char>>();
        for (int i = 0; i < BOARD_HEIGHT; i++)
        {
            List<char> row = new List<char>();
            for (int j = 0; j < BOARD_WIDTH; j++)
            {
                row.Add(' ');
            }
            grid.Add(row);
        }
    }

    public void Print()
    {
        foreach (var row in grid)
        {
            foreach (char c in row)
            {
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }

    public void DrawTriangle(int x, int y, int height)
    {
        for (int i = 0; i < height; ++i)
        {
            int numStars = 2 * i + 1;
            int leftMost = x - i;
            for (int j = 0; j < numStars; ++j)
            {
                int position = leftMost + j;
                if (position >= 0 && position < BOARD_WIDTH && (y + i) < BOARD_HEIGHT && (y + i) >= 0)
                {
                    grid[y + i][position] = '*';
                }
            }
        }
    }
}