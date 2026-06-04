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

        float lerpFactor = Mathf.Clamp01(Time.deltaTime / smoothing);
        smoothedAccel = Vector3.Lerp(smoothedAccel, raw, lerpFactor);

        float x = invertX ?  smoothedAccel.y : -smoothedAccel.y;
        float z = invertY ?  smoothedAccel.x : -smoothedAccel.x;

        rb.AddForce(new Vector3(z, 0f, x) * tiltForce, ForceMode.Acceleration);
    }
}
