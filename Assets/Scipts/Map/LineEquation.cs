using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEquation
{
    public LineEquation(Tuple<int, int> start, Tuple<int, int> end)
    {
        Start = start;
        End = end;

        A = End.Item2 - Start.Item2;
        B = Start.Item1 - End.Item1;
        C = A * Start.Item1 + B * Start.Item2;
    }

    public Tuple<int, int> Start { get; private set; }
    public Tuple<int, int> End { get; private set; }

    public double A { get; private set; }
    public double B { get; private set; }
    public double C { get; private set; }

    public Tuple<double, double> GetIntersectionWithLine(LineEquation otherLine)
    {
        double determinant = A * otherLine.B - otherLine.A * B;

        //lines are parallel
        if (determinant == 0)
        {
            return null;
        }

        //Cramer's Rule

        double x = (otherLine.B * C - B * otherLine.C) / determinant;
        double y = (A * otherLine.C - otherLine.A * C) / determinant;

        return new Tuple<double, double>(x, y);
    }
}


