using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdate : MonoBehaviour
{
    private TCPClient.Player clientplayer;
    internal static GameObject PlayerObj;
    internal static GameObject GenericPlayer;
    internal static CameraMovement mainCamera;
    internal static List<TCPClient.Player> playerList;
    // Use this for initialization
    void Start ()
    {
        PlayerObj = (GameObject)Resources.Load("Models/Prefabs/ClientPlayer");
        GenericPlayer = (GameObject)Resources.Load("Models/Prefabs/NonUserClient");
        clientplayer.PlayerObject = Instantiate(PlayerObj, new Vector3(clientplayer.pos.x, clientplayer.pos.y, clientplayer.pos.z), Quaternion.identity);
        clientplayer.PlayerObject.name = "ClientPlayer";
        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraMovement>(); //works
        mainCamera.reference = clientplayer.PlayerObject.GetComponentsInChildren<Transform>()[1];
        mainCamera.posF = 1.0f;
        mainCamera.rotF = 1.0f;
        mainCamera.enabled = true;
        foreach (TCPClient.Player player in playerList)
        {
            player.PlayerObject = Instantiate(GenericPlayer, player.pos, player.rot);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 p = clientplayer.PlayerObject.transform.position;
        Quaternion q = clientplayer.PlayerObject.transform.rotation;
        string outString = TCPClient.Commands.CommandCreator(Commands.PLANE_POSITION);
        outString += clientplayer.id.ToString() + Delimiters.ATTRIB_DELIM +
                     p.x + Delimiters.ATTRIB_DELIM +
                     p.y + Delimiters.ATTRIB_DELIM +
                     p.z + Delimiters.ATTRIB_DELIM +
                     q.x + Delimiters.ATTRIB_DELIM +
                     q.y + Delimiters.ATTRIB_DELIM +
                     q.z + Delimiters.ATTRIB_DELIM +
                     q.w;
        TCPClient.WriteSocket(outString);
        foreach (TCPClient.Player player in playerList)
        {
            //player.pos is goal position
            //pLoc.position is old position
            Transform pLoc = player.PlayerObject.transform;
            pLoc.position = Vector3.Lerp(pLoc.position, player.pos, 0.35f); //0.35f feels hacky -- consequently works okay
            pLoc.rotation = Quaternion.Lerp(player.PlayerObject.transform.rotation, player.rot, 0.35f);
        }
    }

    public void SetPlayer(TCPClient.Player player)
    {
        clientplayer = player;
    }

    public void SetClientList(List<TCPClient.Player> players)
    {
        playerList = players;
    }

}
