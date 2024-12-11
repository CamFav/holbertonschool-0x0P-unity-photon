using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the player's health, including :
/// taking damage, updating the health UI, and handling death
/// </summary>
public class PlayerHealth : MonoBehaviourPun, IPunObservable
{
    /// <summary>
    /// The maximum health of the player.
    /// </summary>
    public float maxHealth = 100f;

    /// <summary>
    /// The player's current health.
    /// </summary>
    public float currentHealth;

    /// <summary>
    /// The UI slider that represents the player's health bar.
    /// </summary>
    public Slider healthBarSlider;

    /// <summary>
    /// Initializes the player's health to the maximum value and updates the health UI.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    /// <summary>
    /// Reduces the player's health by a specified amount of damage.
    /// If health drops to zero, the player dies.
    /// </summary>
    /// <param name="damage">The amount of damage to apply.</param>
    [PunRPC]
    public void TakeDamage(float damage)
    {
        if (!photonView.IsMine) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Updates the health bar UI to reflect the player's current health.
    /// </summary>
    private void UpdateHealthUI()
    {
        if (healthBarSlider != null)
        {
            // Update the health bar slider's value based on the current health percentage
            healthBarSlider.value = currentHealth / maxHealth;
        }
    }

    /// <summary>
    /// Handles player death, leaving the Photon room and returning to the main menu.
    /// </summary>
    private void Die()
    {
        Debug.Log($"{photonView.Owner.NickName} is dead!");
        PhotonNetwork.LeaveRoom(); // Disconnect the player from the room

        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    /// <summary>
    /// Synchronizes the player's health across all clients.
    /// </summary>
    /// <param name="stream">The Photon stream used for serialization.</param>
    /// <param name="info">Information about the Photon message.</param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send the current health to other clients
            stream.SendNext(currentHealth);
        }
        else
        {
            // Receive the current health from another client
            currentHealth = (float)stream.ReceiveNext();
            UpdateHealthUI(); // Update the health UI after receiving the new health value
        }
    }
}
