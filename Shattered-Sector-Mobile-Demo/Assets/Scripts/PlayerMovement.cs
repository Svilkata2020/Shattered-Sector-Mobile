using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 25f;
    public float wheelRotationSpeed = 300f;

    private float currentSpeed = 0f;
    private float wheelRotationAmount = 0f;
    private int directionFacing = 1;

    private bool isMovingForward = false;
    private bool isMovingBackward = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isRotatingTopLeft = false;
    private bool isRotatingTopRight = false;

    private Rigidbody rb;
    private GameObject top;
    private GameObject[] wheels;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        top = GameObject.FindWithTag("Top");
        wheels = GameObject.FindGameObjectsWithTag("Wheel");
    }

    void MoveTank(int direction, float changeToSpeed, float accelerationSpeed, bool wheelsAreRotating)
    {
        directionFacing = direction;
        currentSpeed = Mathf.MoveTowards(currentSpeed, changeToSpeed, accelerationSpeed * Time.fixedDeltaTime);
        rb.linearVelocity = directionFacing * transform.forward * currentSpeed;
        if (wheelsAreRotating)
        {
            wheelRotationAmount = Mathf.MoveTowards(wheelRotationAmount, -direction * wheelRotationSpeed, accelerationSpeed * 75 * Time.fixedDeltaTime);
        }
        else
        {
            wheelRotationAmount = Mathf.MoveTowards(wheelRotationAmount, 0f, accelerationSpeed * 100 * Time.fixedDeltaTime);
        }
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.rotation *= Quaternion.Euler(0f, 0f, wheelRotationAmount * Time.fixedDeltaTime);
        }
    }

    void FixedUpdate()
    {
        //handles tank movements and & checks for inputs
        if (isMovingBackward)
        {
            
            StopMovingForward();
            StopRotatingRight();
            StopRotatingLeft();
            MoveTank(-1, moveSpeed, 2, true);
        }
        if (isMovingForward)
        {
            StopMovingBackward();
            StopRotatingRight();
            StopRotatingLeft();
            MoveTank(+1, moveSpeed, 2, true);
        }
        if(!isMovingForward && !isMovingBackward)
        {
            MoveTank(directionFacing, 0f, 10, false);
        }

        //handles tank rotation
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

        //handles top rotation
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

        //keep player on ground
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