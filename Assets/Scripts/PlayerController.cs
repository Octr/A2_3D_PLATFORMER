using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;

    public Rigidbody bodyForUse;

    public float moveX, moveY;

    public float movementSpeed, jumpForce, rotationSpeed;

    public bool jumpInput;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector2 clamedInput = new Vector2(moveX, moveY);
        clamedInput = Vector2.ClampMagnitude(clamedInput, 1);

        moveX = clamedInput.x;
        moveY = clamedInput.y;

        if(!jumpInput)
        {
            jumpInput = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0);
        }

    }

    public void FixedUpdate()
    {
        if (jumpInput)
        {
            jumpInput = false;
            if (isGrounded)
            {
                bodyForUse.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
            }
        }

        Vector3 newVelocity = new Vector3();
        newVelocity = bodyForUse.velocity;
        newVelocity.x = moveX * Time.deltaTime * movementSpeed;
        newVelocity.z = moveY * Time.deltaTime * movementSpeed;

        bodyForUse.velocity = newVelocity;

        Vector3 newRotation = new Vector3(newVelocity.x, 0, newVelocity.z);
        if (newRotation.magnitude > 0)
        {
            transform.forward = Vector3.RotateTowards(transform.forward, newRotation, Time.deltaTime * rotationSpeed, 0);
        }
    }
}
