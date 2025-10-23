using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 100f;

    private bool isMovingForward = false;
    private bool isMovingBackward = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;

    void Update()
    {
        // Forward/backward movement (relative to facing direction)
        if (isMovingForward)
        {
            Debug.Log("Moving Forward");
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (isMovingBackward)
        {
            Debug.Log("Moving Backward");
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }

        // Rotation around Y-axis
        if (isRotatingLeft)
        {
            Debug.Log("Rotating Left");
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }

        if (isRotatingRight)
        {
            Debug.Log("Rotating Right");
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        // Keep player anchored vertically (optional safeguard)
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
}