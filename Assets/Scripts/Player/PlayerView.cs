using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public Transform Camera;
        public Animator Animator;
        public Transform GroundCheck;
        public float GroundCheckRadius = 0.4f;
        public LayerMask GroundLayer;

        private PlayerController controller;

        void Start()
        {
            var model = new PlayerModel();
            controller = new PlayerController(model, this);
            controller.Initialize();
        }

        void Update()
        {
            controller.Update();
        }
    }
}
