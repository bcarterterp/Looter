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

    //I am ommiting end points for intersection
    public bool IntersectsAtEdge(LineEquation otherLine)
    {
        Tuple<double, double> intersection = GetIntersectionWithLine(otherLine);

        if (intersection != null) {

            if (IsBetweenTwoPoints(intersection, Start, End)
            && End != otherLine.Start && End != otherLine.End
            && Start != otherLine.Start && Start != otherLine.End) {
                return true;
            }

        }
        else
        {
            bool StartInBetween = IsBetweenTwoPoints(otherLine.Start, Start, End);
            bool EndInBetween = IsBetweenTwoPoints(otherLine.End, Start, End);

            bool StartSameNode = Start == otherLine.Start || Start == otherLine.End;
            bool EndSameNode = End == otherLine.Start || End == otherLine.End;

            if(EndInBetween == false && StartInBetween == false)
            {
                StartInBetween = IsBetweenTwoPoints(Start, otherLine.Start, otherLine.End);
                EndInBetween = IsBetweenTwoPoints(End, otherLine.Start, otherLine.End);

                return !(EndInBetween == false && StartInBetween == false);
            }
            else if(StartInBetween != EndInBetween && StartSameNode != EndSameNode)
            {
                return false;
            }else
            {
                return true;
            }
            
        }

		return false;
	}

    public bool IsBetweenTwoPoints(Tuple<int, int> targetPoint, Tuple<int, int> point1, Tuple<int, int> point2)
    {
        Tuple<double, double> convertedTuple = new Tuple<double, double>(targetPoint.Item1, targetPoint.Item2);
        return IsBetweenTwoPoints(convertedTuple, point1, point2);
    }


    public bool IsBetweenTwoPoints(Tuple<double,double> targetPoint, Tuple<int,int> point1, Tuple<int,int> point2)
	{
		double minX = Mathf.Min(point1.Item1, point2.Item1);
		double minY = Mathf.Min(point1.Item2, point2.Item2);
		double maxX = Mathf.Max(point1.Item1, point2.Item1);
		double maxY = Mathf.Max(point1.Item2, point2.Item2);

		double targetX = targetPoint.Item1;
		double targetY = targetPoint.Item2;

		return minX <= targetX
			&& targetX <= maxX
			&& minY <= targetY
			&& targetY <= maxY;
	}
}


