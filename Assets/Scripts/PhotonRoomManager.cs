using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the connection to the Photon Network, handles :
/// player name input, transitions to the game room.
/// </summary>
public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Input field where the player enters their name.
    /// </summary>
    public TMP_InputField playerNameInput;

    /// <summary>
    /// Button that allows the player to join the game.
    /// </summary>
    public Button joinGameButton;

    /// <summary>
    /// Called when the script instance is first initialized. Disables the join button and connects to Photon.
    /// </summary>
    private void Start()
    {
        joinGameButton.interactable = false; // Disable button initially
        ConnectToPhoton();
    }

    /// <summary>
    /// Connects to the Photon Network. If already connected, enables the join game button.
    /// </summary>
    private void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting to Photon...");
            PhotonNetwork.ConnectUsingSettings(); // Connect to Photon
        }
        else
        {
            Debug.Log("Already connected to Photon.");
            joinGameButton.interactable = true;
        }
    }

    /// <summary>
    /// Called when the client successfully connects to the Photon Master Server.
    /// Enables the join game button.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server.");
        joinGameButton.interactable = true; // Enable button after connection
    }

    /// <summary>
    /// Called when the player clicks the "Join Game" button.
    /// Validates the player's name and joins a Photon lobby.
    /// </summary>
    public void OnJoinGameButtonClicked()
    {
        string playerName = playerNameInput.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Player name is empty!");
            return;
        }

        PhotonNetwork.NickName = playerName; // Set the player's nickname
        Debug.Log($"Player name set to {playerName}. Joining Lobby...");
        PhotonNetwork.JoinLobby(); // Join a lobby
    }

    /// <summary>
    /// Called when the client successfully joins a Photon lobby.
    /// Attempts to create or join a predefined room.
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby. Creating/Joining a room...");
        PhotonNetwork.JoinOrCreateRoom("Room1", new Photon.Realtime.RoomOptions(), Photon.Realtime.TypedLobby.Default);
    }

    /// <summary>
    /// Called when the client successfully joins a room.
    /// Loads the game scene.
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room successfully! Loading GameScene...");
        PhotonNetwork.LoadLevel("GameScene"); // Load the GameScene
    }
}
