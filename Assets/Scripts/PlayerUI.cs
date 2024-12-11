using UnityEngine;

public class PlayerNameUI : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Rotate to face the camera directly
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);

            // Remove roll/pitch rotation
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
