using UnityEngine;

namespace Ashfall.Player
{
    /// <summary>
    /// Basic third-person character movement using CharacterController.
    /// Foundation-first implementation to keep setup simple and stable.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 3.5f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float gravity = -20f;

        [Header("References")]
        [SerializeField] private Transform cameraPivot;

        private CharacterController _controller;
        private Vector3 _velocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
            ApplyGravity();
        }

        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 input = new Vector3(h, 0f, v).normalized;

            if (input.sqrMagnitude < 0.01f) return;

            Vector3 forward = cameraPivot ? cameraPivot.forward : Vector3.forward;
            Vector3 right = cameraPivot ? cameraPivot.right : Vector3.right;
            forward.y = 0f;
            right.y = 0f;

            Vector3 direction = (forward.normalized * input.z + right.normalized * input.x).normalized;
            float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

            _controller.Move(direction * speed * Time.deltaTime);
            transform.forward = Vector3.Slerp(transform.forward, direction, 12f * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (_controller.isGrounded && _velocity.y < 0f)
                _velocity.y = -2f;

            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}
