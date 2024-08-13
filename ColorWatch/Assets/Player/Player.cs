using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerspeed = 1;

    private Rigidbody rb;
    private bool moving;
    private Vector2 moveVector;
    private Vector3 playermove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();

        if (context.started)
        {
            moving = true;
        }
        else if(context.canceled)
        {
            moving = false;
        }
    }

    void Update()
    {
        if (moving)
        {
            playermove = new Vector3(moveVector.x, 0, moveVector.y) * playerspeed;
            rb.AddForce(playermove);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    /*
    void FixedUpdate()
    {
        rb.velocity = playermove * playerspeed;
    }
    */
}
