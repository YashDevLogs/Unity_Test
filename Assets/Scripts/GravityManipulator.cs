using UnityEngine;

public class GravityManipulator : MonoBehaviour
{
    [Header("Gravity Settings")]
    [SerializeField] private float gravityStrength = 9.81f; // gravity force
    [SerializeField] private GameObject hologramPrefab; // Reference to the hologram prefab
    [SerializeField] private float hologramScale = 1.0f; // Scale of the hologram
    [SerializeField] private float hologramHeightOffset = 2.0f; // Height offset for the hologram above the player's head

    [Header("Player and Camera")]
    [SerializeField] private Transform playerTransform; // Reference to the player transform

    private Vector3 gravityDirection = Vector3.down; // Default gravity direction
    private GameObject hologramInstance; // Instance of the hologram in the scene
    private Quaternion hologramTargetRotation = Quaternion.identity; // Target rotation for the hologram

    private void Start()
    {
        if (hologramPrefab != null)
        {
            hologramInstance = Instantiate(hologramPrefab); // Instantiate hologram prefab
            hologramInstance.SetActive(false); // Hide hologram initially
        }
    }

    private void Update()
    {
        HandleGravityDirectionInput();
        HandleApplyGravityInput();

        if (hologramInstance != null && hologramInstance.activeSelf)
        {
            UpdateHologramPosition(); 
        }
    }

    // Handle gravity direction input with arrow keys
    private void HandleGravityDirectionInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            SetGravityDirection(Vector3.forward);
            hologramTargetRotation = Quaternion.Euler(-90f, 0f, 0f); // Rotate -90 degrees around X axis
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            SetGravityDirection(Vector3.back);
            hologramTargetRotation = Quaternion.Euler(90f, 0f, 0f); // Rotate 90 degrees around X axis
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            SetGravityDirection(Vector3.left);
            hologramTargetRotation = Quaternion.Euler(0f, 0f, -90f); // Rotate -90 degrees around Z axis
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            SetGravityDirection(Vector3.right);
            hologramTargetRotation = Quaternion.Euler(0f, 0f, 90f); // Rotate 90 degrees around Z axis
        }
        else
        {
            // Hide hologram if no arrow keys are pressed
            if (hologramInstance != null)
            {
                hologramInstance.SetActive(false);
            }
        }
    }

    // Handle applying the new gravity direction with the Enter key
    private void HandleApplyGravityInput()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Enter key
        {
            ApplyGravityDirection();
        }
    }

    // Set the gravity direction and show the hologram
    private void SetGravityDirection(Vector3 direction)
    {
        gravityDirection = direction.normalized;
        if (hologramInstance != null)
        {
            hologramInstance.SetActive(true); 
            hologramInstance.transform.rotation = hologramTargetRotation; // Apply target rotation to the hologram
            hologramInstance.transform.localScale = new Vector3(hologramScale, hologramScale, hologramScale); // Scale hologram
            UpdateHologramPosition(); // Ensure hologram is positioned above the player's head
        }
    }

    // Update the hologram's position above the player's head
    private void UpdateHologramPosition()
    {
        if (playerTransform != null && hologramInstance != null)
        {
            hologramInstance.transform.position = playerTransform.position + playerTransform.up * hologramHeightOffset;
        }
    }

    // Apply the selected gravity direction and rotate player and camera
    private void ApplyGravityDirection()
    {
        // Apply new gravity direction
        Physics.gravity = gravityDirection * gravityStrength;

        // Apply rotation to the player 
        if (playerTransform != null)
        {
            playerTransform.rotation = hologramTargetRotation;
        }
    }
}
