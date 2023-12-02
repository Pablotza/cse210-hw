using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Square square = new Square("Red", 5.0);
        Rectangle rectangle = new Rectangle("Blue", 4.0, 6.0);
        Circle circle = new Circle("Green", 3.0);

        List<Shape> shapes = new List<Shape>
        {
            square,
            rectangle,
            circle
        };

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.Color}, Area: {shape.GetArea()}");
            Console.WriteLine();
        }

        Console.ReadKey();
    }
}
