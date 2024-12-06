using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonConnectionTest : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log("Connecting to Photon...");
        PhotonNetwork.ConnectUsingSettings(); // Starts the connection to Photon Cloud
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Cloud!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError($"Disconnected from Photon Cloud: {cause}");
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to Photon Server!");
    }
}
