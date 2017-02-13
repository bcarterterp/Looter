using System.Collections.Generic;

public class Node
{

    private int id;
    private Tuple<int, int> coord;
    private string name;
    private Dictionary<int, Node> linkedNodes;
    private bool generatedNeighbors;

    public Node(int nodeID)
    {
        id = nodeID;
        name = "Node" + id;
        linkedNodes = new Dictionary<int, Node>();
        generatedNeighbors = false;
    }

    public void LinkNodes(Node node)
    {
        linkedNodes[node.GetID()] = node;
    }

    public void AddInvalidCoords(List<LineEquation> lineList)
    {
        foreach (Node node in linkedNodes.Values)
        {
            LineEquation neighborLine = new LineEquation(coord, node.GetCoords());
            int index = -1;
            for (int i = 0; i < lineList.Count; i++)
            {
                LineEquation centerLine = lineList[i];
                Tuple<double, double> intersection = neighborLine.GetIntersectionWithLine(centerLine);
                if (intersection != null)
                {
                    if (!(coord == centerLine.End) && !(node.GetCoords() == centerLine.End))
                    {
                        index = i;
                    }
                }
            }
            if(index != -1)
            {
                lineList.RemoveAt(index);
            }
        }
    }

    public void SetCoords(Tuple<int, int> coordinates)
    {
        coord = coordinates;
    }

    public Tuple<int, int> GetCoords()
    {
        return coord;
    }

    public int GetXCoord()
    {
        return coord.Item1;
    }

    public int GetYCoord()
    {
        return coord.Item2;
    }

    public string GetName()
    {
        return name;
    }

    public int GetID()
    {
        return id;
    }

    public Dictionary<int, Node> GetLinkedNodes()
    {
        return linkedNodes;
    }

    public bool IsLinkedNode(int id)
    {
        return linkedNodes.ContainsKey(id);
    }

    public bool HasGeneratedNeighbors()
    {
        return generatedNeighbors;
    }

    public void SetGeneratedNeighbors(bool hasGenerated)
    {
        generatedNeighbors = hasGenerated;
    }

    public override string ToString()
    {
        return string.Format("{0}:({1},{2})", name, GetXCoord(), GetYCoord());
    }
}
