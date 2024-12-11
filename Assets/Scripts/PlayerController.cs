using Photon.Pun;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls the player's movement, animations, shooting, and name display in a Photon networked game.
/// </summary>
public class PlayerController : MonoBehaviourPun
{
    /// <summary>
    /// UI Text component for displaying the player's name.
    /// </summary>
    public TMP_Text playerNameText;

    /// <summary>
    /// Speed at which the player moves forward or backward.
    /// </summary>
    public float moveSpeed = 5f;

    /// <summary>
    /// Speed at which the player rotates.
    /// </summary>
    public float rotationSpeed = 200f;

    /// <summary>
    /// Prefab for the bullet to be instantiated when the player shoots.
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Transform indicating the position and direction from which bullets are shot.
    /// </summary>
    public Transform shootPoint;

    /// <summary>
    /// Speed at which bullets travel after being fired.
    /// </summary>
    public float bulletSpeed = 10f;

    /// <summary>
    /// Animator component to handle idle and walking animations.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Initializes the player. Sets the player's displayed name based on Photon ownership.
    /// </summary>
    void Start()
    {
        if (photonView.IsMine)
        {
            playerNameText.text = PhotonNetwork.NickName;
        }
        else
        {
            playerNameText.text = photonView.Owner.NickName;
        }
    }

    /// <summary>
    /// Updates the player's movement, animation, and shooting controls.
    /// </summary>
    void Update()
    {
        if (!photonView.IsMine) return;

        HandleMovement();
        HandleShooting();
    }

    /// <summary>
    /// Handles player movement and rotation based on input. Updates the animation state.
    /// </summary>
    private void HandleMovement()
    {
        // Get input for movement and rotation
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate forward/backward movement
        Vector3 movement = transform.forward * vertical * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // Rotate the player left/right
        float rotation = horizontal * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        // Update the Animator's isWalking parameter
        bool isWalking = vertical != 0 || horizontal != 0;
        animator.SetBool("isWalking", isWalking);
    }

    /// <summary>
    /// Checks for player input to fire bullets and calls the Shoot method.
    /// </summary>
    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to shoot
        {
            Shoot();
        }
    }

    /// <summary>
    /// Instantiates a bullet at the shoot point and applies velocity to it.
    /// </summary>
    private void Shoot()
    {
        if (bulletPrefab == null || shootPoint == null)
        {
            Debug.LogError("Bullet prefab or shoot point is not assigned!");
            return;
        }

        // Instantiate the bullet across the network
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, shootPoint.position, shootPoint.rotation);

        // Add velocity to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * bulletSpeed;
    }
}
