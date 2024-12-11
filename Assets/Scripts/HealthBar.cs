using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's health bar UI
/// </summary>
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// The UI slider representing the health bar.
    /// </summary>
    public Slider healthBar;

    private PlayerHealth playerHealth;

    /// <summary>
    /// Initializes the health bar to match the player's maximum health at the start of the game.
    /// </summary>
    private void Start()
    {
        // Find the PlayerHealth component in the parent object
        playerHealth = GetComponentInParent<PlayerHealth>();
        if (playerHealth != null)
        {
            // Set the maximum value of the health bar and initialize it to full health
            healthBar.maxValue = playerHealth.maxHealth;
            healthBar.value = playerHealth.maxHealth;
        }
    }

    /// <summary>
    /// Updates the health bar to reflect the player's current health.
    /// </summary>
    private void Update()
    {
        if (playerHealth != null)
        {
            // Ensure the health bar slider reflects the current health value
            healthBar.value = playerHealth.currentHealth;
        }
    }
}
