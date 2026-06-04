using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float tiltForce = 20f;
    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;
    [SerializeField] private float smoothing = 0.15f;

    private Rigidbody rb;
    private Vector3 smoothedAccel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Explicitly enable the accelerometer
        if (Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);
    }

    private void FixedUpdate()
    {
        ApplyTilt();
    }

    private void ApplyTilt()
    {
        if (Accelerometer.current == null) return;

        Vector3 raw = Accelerometer.current.acceleration.ReadValue();

        // Smooth the raw reading to reduce jitter
        smoothedAccel = Vector3.Lerp(smoothedAccel, raw, Time.deltaTime * smoothing);

        // Landscape axis remapping + optional invert
        float x = invertX ? smoothedAccel.y : -smoothedAccel.y;
        float z = invertY ? smoothedAccel.x : -smoothedAccel.x;

        rb.AddForce(new Vector3(x, 0f, z) * tiltForce, ForceMode.Acceleration);
    }
}
