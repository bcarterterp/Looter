using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Map map = new Map();
        Node node = new Node(0);
        node.SetCoords(new Tuple<int, int>(0, 0));
        map.nodes.Add(0, node);

        Node node1 = new Node(1);
        node1.SetCoords(new Tuple<int, int>(-3, 2));
        map.nodes.Add(1, node1);

		Node node2 = new Node(2);
		node2.SetCoords(new Tuple<int, int>(3, 2));
		map.nodes.Add(2, node2);

		Node node3 = new Node(3);
		node3.SetCoords(new Tuple<int, int>(-2, -2));
		map.nodes.Add(3, node3);

		node1.LinkNodes (node2);
		node2.LinkNodes (node1);

		LineEquation line1 = new LineEquation (node.GetCoords (), node3.GetCoords ());
		LineEquation line2 = new LineEquation (node1.GetCoords (), node2.GetCoords ());
		Debug.Log("DIntersect: "+line1.IntersectsAtEdge(line2));

		List<LineEquation> list = map.GenerateIntersecter (node);
        foreach (LineEquation line in list)
        {
			Debug.Log (line.End);
			Debug.Log("DIntersect: "+line.IntersectsAtEdge(line2));
        }
    }
}
