using System.Collections;
using UnityEngine;
namespace Assets.Scripts.Player
{
    public class PlayerModel
    {
        public float movementSpeed = 5f;
        public float jumpForce = 5f;
        public bool isGrounded;
        public bool isFalling;
        public float fallTime;

        public float mouseSensitivity = 100f;
        public float xRotation = 0f;
        public float TopClamp = -90f;
        public float BottomClamp = 90f;
    }
}

