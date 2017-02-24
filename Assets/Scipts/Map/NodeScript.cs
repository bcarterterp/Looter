using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour{

    public Button nodeButton;
	public Sprite inactiveNode, activeNode;

    private NodeSelectedListener listener;
    private int nodeID;

    public void SetNodeSelectedListener(NodeSelectedListener nodeSelected, int ID){
        listener = nodeSelected;
        nodeID = ID;
    }

    public void NodeClicked()
    {
        listener.NodeSelected(nodeID);
    }

	public void SetAsActiveNode(){
		nodeButton.image.sprite = activeNode;
	}

}
