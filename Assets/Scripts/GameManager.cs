using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// Manages the overall game state :
/// player instantiation, game start, and win condition checks.
/// </summary>
public class GameManager : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// The prefab used to instantiate player characters in the game.
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// The UI Canvas displayed to show the "You Win!" message.
    /// </summary>
    public Canvas winMessageCanvas;

    private bool gameStarted = false; // Tracks whether the game has officially started
    private bool gameEnded = false;  // Tracks whether the game has ended

    /// <summary>
    /// Called when the game manager starts. Instantiates the player character and initializes the game.
    /// </summary>
    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            // Instantiate the player prefab at the start of the game
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
            Invoke("StartGame", 5f);
        }
        else
        {
            Debug.LogError("Not connected to Photon Network");
        }
    }

    /// <summary>
    /// Initializes the game logic and enables win condition checks.
    /// </summary>
    private void StartGame()
    {
        gameStarted = true;
        Debug.Log("Game started");
    }

    /// <summary>
    /// Updates the game state each frame, including checking for win conditions.
    /// </summary>
    private void Update()
    {
        if (gameStarted && !gameEnded)
        {
            CheckGameEnd();
        }
    }

    /// <summary>
    /// Checks whether only one player remains alive and determines the winner.
    /// </summary>
    private void CheckGameEnd()
    {
        if (!PhotonNetwork.IsMasterClient) return; // Only the MasterClient runs the win condition check

        int alivePlayersCount = 0;
        PhotonView winnerPhotonView = null;

        // Iterate through all player objects in the scene
        foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();

            if (playerHealth != null && playerHealth.currentHealth > 0)
            {
                alivePlayersCount++;
                winnerPhotonView = playerHealth.photonView;
            }
        }

        // If only one player is alive, show the "You Win!" message to that player
        if (alivePlayersCount == 1 && winnerPhotonView != null)
        {
            gameEnded = true; // Mark the game as ended

            if (winnerPhotonView.IsMine)
            {
                DisplayWinMessage();
            }
        }
    }

    /// <summary>
    /// Displays the "You Win!" message for the winning player.
    /// </summary>
    private void DisplayWinMessage()
    {
        if (winMessageCanvas != null)
        {
            winMessageCanvas.gameObject.SetActive(true); // Activate the win message canvas
            Debug.Log("You Win!");
        }
    }

    /// <summary>
    /// Called when another player leaves the room. Triggers a recheck for the win condition.
    /// </summary>
    /// <param name="otherPlayer">The player who left the room.</param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CheckGameEnd(); // Recheck win condition when a player leaves
    }
}