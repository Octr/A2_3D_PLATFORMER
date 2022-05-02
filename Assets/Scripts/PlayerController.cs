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

    public Animator animController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        Quaternion camRotation = Quaternion.LookRotation(camForward);

        Vector3 clampedInput = new Vector3(moveX, 0, moveY);
        clampedInput = camRotation * clampedInput;

        clampedInput = Vector3.ClampMagnitude(clampedInput, 1);

        moveX = clampedInput.x;
        moveY = clampedInput.z;

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

        Vector2 movementInput = new Vector2(moveX, moveY);

        float animationRunningSpeed = movementInput.magnitude;
        animController.SetFloat("Blend", animationRunningSpeed);

        //if (newRotation.magnitude > 0.5f || newRotation.magnitude < -0.5f)
        {
            transform.forward = Vector3.RotateTowards(transform.forward, newRotation, Time.deltaTime * rotationSpeed, 0);
        }
    }
}
