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
        node = new Node(1);
        node.SetCoords(new Tuple<int, int>(1, 1));
        map.nodes.Add(1, node);
        List<LineEquation> list = map.GetOffLimitCoords(node);
        foreach (LineEquation line in list)
        {
            Debug.Log(line.End);
        }
    }
}
