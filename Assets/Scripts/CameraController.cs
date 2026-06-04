using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private float followHeight = 10f;
    [SerializeField] private float followDistance = 8f;
    [SerializeField] private float followSmoothing = 0.1f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float minFOV = 20f;
    [SerializeField] private float maxFOV = 80f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 0.3f;
    [SerializeField] private float rotationSmoothing = 0.1f;

    private Camera mainCamera;
    private float targetAngle = 0f;
    private float currentAngle = 0f;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        HandleTwoFingerInput();
        Follow();
    }

    private void Follow()
    {
        // Smoothly chase the orbit angle toward the target angle
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime / rotationSmoothing);
        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 desiredPosition = target.position + new Vector3(Mathf.Sin(rad) * followDistance, followHeight, -Mathf.Cos(rad) * followDistance);

        // Smoothly chase the ball position rather than snapping
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime / followSmoothing);
        transform.LookAt(target);
    }

    private void HandleTwoFingerInput()
    {
        if (Input.touchCount != 2) return;

        Touch t0 = Input.GetTouch(0);
        Touch t1 = Input.GetTouch(1);

        Vector2 t0Prev = t0.position - t0.deltaPosition;
        Vector2 t1Prev = t1.position - t1.deltaPosition;

        // Zoom
        float prevDist = Vector2.Distance(t0Prev, t1Prev);
        float currDist = Vector2.Distance(t0.position, t1.position);
        float pinchDelta = prevDist - currDist;

        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView + pinchDelta * zoomSpeed, minFOV, maxFOV);

        // Rotation
        float prevAngle = Mathf.Atan2(t1Prev.y - t0Prev.y, t1Prev.x - t0Prev.x) * Mathf.Rad2Deg;
        float currAngle = Mathf.Atan2(t1.position.y - t0.position.y, t1.position.x - t0.position.x) * Mathf.Rad2Deg;
        float twistDelta = Mathf.DeltaAngle(prevAngle, currAngle);
        
        targetAngle -= twistDelta * rotationSpeed;
    }
}
