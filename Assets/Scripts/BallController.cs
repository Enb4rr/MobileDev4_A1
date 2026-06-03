using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 accel = Input.acceleration;
        Vector3 force = new Vector3(accel.y, 0f, -accel.x);

        rb.AddForce(force * speed);
    }
}
