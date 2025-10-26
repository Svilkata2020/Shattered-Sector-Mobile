using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 25f;

    private bool isMovingForward = false;
    private bool isMovingBackward = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isRotatingTopLeft = false;
    private bool isRotatingTopRight = false;

    private Rigidbody rb;
    private GameObject top;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        top = GameObject.FindWithTag("Top");
    }

    void FixedUpdate()
    {
        if (isMovingBackward)
        {
            StopMovingForward();
            StopRotatingRight();
            StopRotatingLeft();
            rb.linearVelocity = -transform.forward * moveSpeed;
        }
        if (isMovingForward)
        {
            StopMovingBackward();
            StopRotatingRight();
            StopRotatingLeft();
            rb.linearVelocity = transform.forward * moveSpeed;
        }

        float rotationInput = 0f;
        if (isRotatingLeft)
        {
            StopMovingForward();
            StopMovingBackward();
            StopRotatingRight();
            rotationInput = -1f;
        }

        if (isRotatingRight)
        {
            StopMovingForward();
            StopMovingBackward();
            StopRotatingLeft();
            rotationInput = +1f;
        }

        if (rotationInput != 0f)
        {
            float rotationAmount = rotationInput * rotationSpeed * Time.fixedDeltaTime;
            Quaternion delta = Quaternion.Euler(0f, rotationAmount, 0f);
            rb.MoveRotation(rb.rotation * delta);
        }

        float topRotationInput = 0f;
        if (isRotatingTopLeft)
        {
            StopRotatingTopRight();
            topRotationInput = -1f;
        }

        if (isRotatingTopRight)
        {
            StopRotatingTopLeft();
            topRotationInput = +1f;
        }

        if (topRotationInput != 0f)
        {
            top.transform.rotation *= Quaternion.Euler(0f, topRotationInput * rotationSpeed * Time.fixedDeltaTime, 0f);
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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
    public void StartRotatingTopLeft() => isRotatingTopLeft = true;
    public void StopRotatingTopLeft() => isRotatingTopLeft = false;
    public void StartRotatingTopRight() => isRotatingTopRight = true;
    public void StopRotatingTopRight() => isRotatingTopRight = false;

}