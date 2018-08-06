using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBuilder : MonoBehaviour
{
    GameObject bridge;
    GameObject support;
    List<GameObject> bridgeList;
    Vector3 StartPosition;
	// Use this for initialization
	void Start ()
    {
        StartPosition = new Vector3(0, 0, 0);
        bridge = (GameObject)Resources.Load("Map/Prefabs/WideBridge");
        support = (GameObject)Resources.Load("Map/Prefabs/Support");
        bridgeList = new List<GameObject>();
        for(int i = 0; i < 15; i++)
        {
            if(i % 5 == 0)
            {
                bridgeList.Add(Instantiate(support, StartPosition, bridge.transform.rotation));
            } else
            {
                bridgeList.Add(Instantiate(bridge, StartPosition, bridge.transform.rotation));

            }
            StartPosition = new Vector3(StartPosition.x, StartPosition.y, 50 + (i * 50));
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
