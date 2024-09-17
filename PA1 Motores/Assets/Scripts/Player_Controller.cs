using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [Header("Player Properties")]
    private Rigidbody myRBD;
    [SerializeField] Vector2 direction;
    [SerializeField] float xMovement;
    [SerializeField] float speed;
    [SerializeField] float zMovement;
    [SerializeField] float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 1.1f;
    private bool isGrounded;

    private void Awake()
    {
        myRBD = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        OnMove();
        CheckGroundStatus();
    }
    public void OnMove()
    {
        myRBD.velocity = new Vector3(xMovement * speed, myRBD.velocity.y, zMovement * speed);
    }
    public void OnMovementX(InputAction.CallbackContext context)
    {
        xMovement = context.ReadValue<float>();
    }
    public void OnMovementZ(InputAction.CallbackContext context)
    {
        zMovement = context.ReadValue<float>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            myRBD.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
