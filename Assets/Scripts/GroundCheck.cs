using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController controller;

    public List<Collider> onGroundColliders = new List<Collider>();

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            Debug.Log("Is Ground");

            onGroundColliders.Add(other);
            controller.hovering = false;
            controller.doubleJumped = false;
            controller.isGrounded = true;
            controller.animController.SetBool("Jumping", false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        onGroundColliders.Remove(other);
        if(onGroundColliders.Count <= 0)
        {
            controller.isGrounded = false;
        }
    }
}