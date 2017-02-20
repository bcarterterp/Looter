using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour{

    Button nodeButton;
    NodeSelectedListener listener;
    int nodeID;

    public void SetNodeSelectedListener(NodeSelectedListener nodeSelected, int ID){
        listener = nodeSelected;
        nodeID = ID;
    }

    public void NodeClicked()
    {
        listener.NodeSelected(nodeID);
    }

}
