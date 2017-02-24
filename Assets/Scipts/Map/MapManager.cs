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

	private Dictionary<int, NodeScript> nodeButtons;
	private Map map;

	// Use this for initialization
	void Start () {
        map = new Map();
		Node[] nodes = map.GetNodes ();
		nodeButtons = new Dictionary<int, NodeScript> ();
		foreach (Node node in nodes) {
			GenerateNewNodeButton (node);
		}
    }

    public void NodeSelected(int nodeID)
    {
        Node[] newNodes = map.MoveToNode(nodeID);
		if (newNodes != null) {
			nodeButtons [nodeID].SetAsActiveNode ();
			foreach (Node node in newNodes) {
				GenerateNewNodeButton (node);
			}
		}
    }

	private void GenerateNewNodeButton(Node node){
		Tuple<int,int> coords = node.GetCoords ();
		float x = coords.Item1 * UNIT_SCALE;
		float y = coords.Item2 * UNIT_SCALE;
		Button 	nodeButton = Instantiate (nodePrefab).GetComponent<Button>();
		nodeButton.transform.position = new Vector3(x, y);
		nodeButton.transform.SetParent (canvas.transform, false);
		nodeButton.GetComponentInChildren<Text>().text = node.GetName ();
		NodeScript script = nodeButton.GetComponentInChildren<NodeScript> ();
		script.SetNodeSelectedListener(this, node.GetID());
		nodeButtons.Add (node.GetID(), script);
	}
}
