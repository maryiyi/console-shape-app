using System.Drawing;

namespace ConsoleApp1;

public abstract class Shape
{
    public Color Color;
    public int ShapeId;
    public int Position;
    public FillMode fillMode;

    public abstract void select();
    public abstract void remove();
    public abstract void edit();
    public abstract void paint();
    public abstract void draw();
}