using UnityEngine;

namespace Ashfall.Camera
{
    /// <summary>
    /// Simple orbit camera to keep foundation systems testable before cinematic camera pass.
    /// </summary>
    public class ThirdPersonCameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new(0f, 1.8f, -3.5f);
        [SerializeField] private float sensitivity = 120f;
        [SerializeField] private float smoothTime = 0.08f;

        private float _yaw;
        private float _pitch = 15f;
        private Vector3 _velocity;

        private void LateUpdate()
        {
            if (!target) return;

            _yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            _pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, -20f, 60f);

            Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);
            Vector3 desiredPos = target.position + rotation * offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref _velocity, smoothTime);
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }
    }
}
