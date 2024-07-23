using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController
    {
        private PlayerModel model;
        private PlayerView view;

        public PlayerController(PlayerModel model, PlayerView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void Update()
        {
            HandleMouseMovement();
            PlayerMovement();
            PlayerJump();
            CheckGroundStatus();
        }

        private void PlayerMovement()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = view.transform.right * x + view.transform.forward * z;
            view.Rigidbody.MovePosition(view.Rigidbody.position + move * model.movementSpeed * Time.deltaTime);

            bool isRunning = move.magnitude > 0;
            view.Animator.SetBool("IsRunning", isRunning);
        }

        private void PlayerJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && model.isGrounded)
            {
                Vector3 jumpDirection = -Physics.gravity.normalized; // Use the opposite direction of gravity
                view.Rigidbody.AddForce(jumpDirection * model.jumpForce, ForceMode.Impulse);
            }
        }

        private void HandleMouseMovement()
        {
            float mouseX = Input.GetAxis("Mouse X") * model.mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * model.mouseSensitivity * Time.deltaTime;

            model.xRotation -= mouseY;
            model.xRotation = Mathf.Clamp(model.xRotation, model.TopClamp, model.BottomClamp);

            view.Camera.localRotation = Quaternion.Euler(model.xRotation, 0f, 0f);
            view.transform.Rotate(Vector3.up * mouseX);
        }

        private void CheckGroundStatus()
        {
            model.isGrounded = Physics.CheckSphere(view.GroundCheck.position, view.GroundCheckRadius, view.GroundLayer);

            if (!model.isGrounded)
            {
                if (!model.isFalling)
                {
                    model.isFalling = true;
                    view.StartCoroutine(FallTimer());
                }
            }
            else
            {
                model.isFalling = false;
                model.fallTime = 0f;
            }

            view.Animator.SetBool("IsFalling", !model.isGrounded);
        }

        private IEnumerator FallTimer()
        {
            model.fallTime = 0f;

            while (model.isFalling)
            {
                model.fallTime += Time.deltaTime;

                if (model.fallTime >= 4f)
                {
                    EventManager.PlayerDeath();
                    yield break;
                }

                yield return null;
            }
        }
    }
}
