using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementSpeed = 5;
    private float jumpForce = 5;
    private bool isGrounded;

    private float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private float TopClamp = -90f;
    private float BottomClamp = 90f;

    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform playerCamera; // Reference to the player's camera
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform groundCheck; // Reference to the GroundCheck GameObject
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleMouseMovement();
        PlayerMovement();
        PlayerJump();
        CheckGroundStatus();
    }

    // Player Movement Based on the Input
    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal"); // Horizontal input
        float z = Input.GetAxis("Vertical"); // Vertical Input

        Vector3 move = transform.right * x + transform.forward * z;
        playerRb.MovePosition(playerRb.position + move * movementSpeed * Time.deltaTime);

        bool isRunning = move.magnitude > 0; // Check if there is any movement
        playerAnim.SetBool("IsRunning", isRunning);
    }

    // Player Jump Logic based on Input
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Player Look Around based on Mouse Input
    private void HandleMouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, TopClamp, BottomClamp);

        // Rotate the camera up and down
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body left and right
        transform.Rotate(Vector3.up * mouseX);
    }

    // Check if the player is grounded using the GroundCheck GameObject
    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (!isGrounded)
        {
            Debug.Log("is not grounded");
            playerAnim.SetBool("IsFalling", true);
        }
        else if (isGrounded)
        {
            Debug.Log("is grounded");
            playerAnim.SetBool("IsFalling", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
