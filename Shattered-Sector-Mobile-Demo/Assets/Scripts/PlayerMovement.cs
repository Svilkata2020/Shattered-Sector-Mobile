using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 25f;

    private bool isMovingForward = false;
    private bool isMovingBackward = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isMovingBackward)
        {
            StopMovingForward();
            rb.linearVelocity = -transform.forward * moveSpeed;
        }
        if (isMovingForward)
        {
            StopMovingBackward();
            rb.linearVelocity = transform.forward * moveSpeed;
        }
        if(isRotatingLeft)
        {
            StopRotatingRight();
        }
        if(isRotatingRight)
        {
            StopRotatingLeft();
        }

    }

    // Movement controls
    public void StartMovingForward() => isMovingForward = true;
    public void StopMovingForward() => isMovingForward = false;

    public void StartMovingBackward() => isMovingBackward = true;
    public void StopMovingBackward() => isMovingBackward = false;

    // Rotation controls
    public void StartRotatingLeft() => isRotatingLeft = true;
    public void StopRotatingLeft() => isRotatingLeft = false;

    public void StartRotatingRight() => isRotatingRight = true;
    public void StopRotatingRight() => isRotatingRight = false;
}