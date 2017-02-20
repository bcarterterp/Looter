using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map
{

    private int activeNodeID, id;
    private Dictionary<int, Node> nodes;

    public Map()
    {
        id = 0;
        activeNodeID = id;
        nodes = new Dictionary<int, Node>();
        Node home = new Node(activeNodeID);
        home.SetCoords(new Tuple<int, int>(0, 0));
        nodes.Add(id, home);
    }

	public Node GetNode(int nodeID){
		return nodes [nodeID];
	}

	public Node[] MoveToNode(int nodeID){
		if (IsValidMove (nodeID)) {
			Node node = nodes [nodeID];
			int nodeIndex = nodes.Count;
			GenerateNeighbors (node);
			if (nodes.Count > nodeIndex) {
				Node[] returnedList = nodes.Values.ToArray();
				return returnedList.Skip (nodeIndex).Take (nodes.Count - 1).ToArray ();
			}
		}
		return null;
	}

	public bool IsValidMove(int nodeID){
		Node node = nodes [activeNodeID];
		return node.isNeighbor (nodeID);
	}

	public void GenerateNeighbors(Node node)
	{
		int numberOfNeighbors = Random.Range(1, 5);
		List<LineEquation> validLocations = GetOffLimitCoords(node);
		if(validLocations.Count < numberOfNeighbors)
		{
			numberOfNeighbors = validLocations.Count;
		}
		for (int i = 0; i < numberOfNeighbors; i++)
		{
			Node newNode = GenerateNeighbor(validLocations, node);
			node.LinkNodes(newNode);
			newNode.LinkNodes(node);
		}
		node.SetGeneratedNeighbors(true);
	}

	public List<LineEquation> GetOffLimitCoords(Node centerNode)
	{
		List<LineEquation> lineList = GenerateIntersecter(centerNode);
		foreach (Node node in nodes.Values)
		{
			int xDistance = Mathf.Abs(centerNode.GetXCoord() - node.GetXCoord());
			int yDistance = Mathf.Abs(centerNode.GetYCoord() - node.GetYCoord());
			if (xDistance <= 4 || yDistance <= 4)
			{
				node.AddInvalidCoords(lineList);
			}
		}
		return lineList;
	}

	public List<LineEquation> GenerateIntersecter(Node centerNode)
	{
		List<LineEquation> lineList = new List<LineEquation>();
		int x = centerNode.GetXCoord();
		int y = centerNode.GetYCoord();
		Tuple<int, int> start = new Tuple<int, int>(x, y);
		for (int i = -2; i < 3; i++)
		{
			for (int j = -2; j < 3; j++)
			{
				Tuple<int, int> end = new Tuple<int, int>(x + i, y + j);
				LineEquation line = new LineEquation(start, end);
				if (i != 0 || j != 0)
				{
					lineList.Add(line);
				}
			}
		}
		return lineList;
	}

    private Node GenerateNeighbor(List<LineEquation> validLocations, Node centerNode)
    {
        Node node = null;
        int option = Random.Range(0, validLocations.Count);
        LineEquation line = validLocations[option];
        Tuple<int, int> nodeCoord = line.End;
        foreach (Node oldNode in nodes.Values)
        {
            if (nodeCoord.Equals(oldNode.GetCoords()))
            {
                node = oldNode;
            }
        }
        if (node == null)
        {
            node = new Node(++id);
            node.SetCoords(nodeCoord);
            nodes.Add(node.GetID(), node);
        }

        int xRange = nodeCoord.Item1;
        int yRange = nodeCoord.Item2;
        if (Mathf.Abs(xRange) == 2 && Mathf.Abs(yRange) == 2)
        {
            int oneUnitY = xRange > 0 ? 1 : -1;
            int oneUnitX = yRange > 0 ? 1 : -1;
            int x = oneUnitX + centerNode.GetXCoord();
            int y = oneUnitY + centerNode.GetYCoord();
            nodeCoord = new Tuple<int, int>(x, y);
            int index = -1;
            for(int i = 0; i < validLocations.Count; i++)
            {
                if(nodeCoord == validLocations[i].End)
                {
                    index = i;
                }
            }
            if(index > -1)
            {
                validLocations.RemoveAt(index);
            }
        }
        return node;
    }

    public void GenerateDebugMap()
    {
        int index = 0;
        for (int i = 0; i < 2; i++)
        {
            int nodeCount = nodes.Count;
            for (int j = index; j < nodeCount; j++)
            {
                GenerateNeighbors(nodes[j]);
            }
            index = nodeCount;
        }
        foreach (Node node in nodes.Values)
        {
            foreach (Node linkedNode in node.GetLinkedNodes().Values)
            {
                string linkText = string.Format("{0} -> {1}", node, linkedNode);
                Debug.Log(linkText);
            }
        }
    }

	public Node[] GetNodes(){
		return nodes.Values.ToArray();
	}
}
