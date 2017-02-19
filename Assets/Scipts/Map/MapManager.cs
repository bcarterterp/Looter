using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour {

	public GameObject nodePrefab;
	public Canvas canvas;
	public Sprite unoccupiedNode, occupiedNode;

	private Dictionary<Button, int> nodeButtons;

	// Use this for initialization
	void Start () {
        Map map = new Map();
		Node[] nodes = map.GetNodes ();
		nodeButtons = new Dictionary<Button, int> ();
		foreach (Node node in nodes) {
			float x = 0;
			float y = 0;
			Tuple<int,int> coords = node.GetCoords ();
			x += coords.Item1 * 100;
			y += coords.Item2 * 100;
			Debug.Log ("x" + x +", y" + y);
			Button 	nodeButton = Instantiate (nodePrefab).GetComponent<Button>();
			nodeButton.transform.position = new Vector3(x, y);
			nodeButton.transform.SetParent (canvas.transform, false);
			nodeButton.GetComponentInChildren<Text>().text = node.GetName ();
			nodeButtons.Add (nodeButton, node.GetID());
		}
    }

	private void MoveToNewNode(){

	}
}
