using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour, NodeSelectedListener {

    public const int UNIT_SCALE = 100;

	public GameObject nodePrefab;
	public Canvas canvas;
	public Sprite unoccupiedNode, occupiedNode;

	private Dictionary<Button, int> nodeButtons;
	private Map map;

	// Use this for initialization
	void Start () {
        map = new Map();
		Node[] nodes = map.GetNodes ();
		nodeButtons = new Dictionary<Button, int> ();
		foreach (Node node in nodes) {
			float x = 0;
			float y = 0;
			Tuple<int,int> coords = node.GetCoords ();
			x += coords.Item1 * UNIT_SCALE;
			y += coords.Item2 * UNIT_SCALE;
			Button 	nodeButton = Instantiate (nodePrefab).GetComponent<Button>();
			nodeButton.transform.position = new Vector3(x, y);
			nodeButton.transform.SetParent (canvas.transform, false);
			nodeButton.GetComponentInChildren<Text>().text = node.GetName ();
            nodeButton.GetComponentInChildren<NodeScript>().SetNodeSelectedListener(this, node.GetID());
			nodeButtons.Add (nodeButton, node.GetID());
		}
    }

    public void NodeSelected(int nodeID)
    {
        Node[] newNodes = map.MoveToNode(nodeID);
        foreach (Node node in newNodes)
        {

        }
    }
}
