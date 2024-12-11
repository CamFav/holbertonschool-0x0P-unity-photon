using Photon.Pun;
using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the behavior of the bullet :
/// lifetime, collision effects, and damage.
/// </summary>
public class BulletBehaviour : MonoBehaviourPun
{
    /// <summary>
    /// Damage
    /// </summary>
    public float damage = 20f; 

    /// <summary>
    /// The time in seconds before the bullet is automatically destroyed
    /// </summary>
    public float lifetime = 5f;

    /// <summary>
    /// Called when the bullet is first instantiated. Destroy the bullet after its lifetime.
    /// </summary>
    private void Start()
    {
        // Start the coroutine to destroy the bullet
        StartCoroutine(DestroyAfterTime(lifetime));
    }

    /// <summary>
    /// Coroutine that destroys the bullet after a specified time.
    /// </summary>
    /// <param name="time">The time in seconds to wait before destroying the bullet.</param>
    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        // Destroy the bullet after the specified time
        PhotonNetwork.Destroy(gameObject);
    }

    /// <summary>
    /// Called when the bullet collides with another object.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet hits a player, apply damage and destroy the bullet
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            // Apply damage to the player using RPC
            playerHealth.photonView.RPC("TakeDamage", RpcTarget.All, damage);
        }

        // Destroy the bullet after it hits a player
        PhotonNetwork.Destroy(gameObject);

        // Stop the destroy timer if the bullet collides with something
        StopCoroutine(DestroyAfterTime(lifetime));
    }
}
