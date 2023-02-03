using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    [Header("Mobility")]
    public Rigidbody2D rb;
    private Vector2 moveVector;
    public float jump = 0;
    public float speed = 0;
    public float impulse = 0;
    private bool facingrigth = true;
    private float horizontal;

    [Header("groundCheck")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        
    }
      void Update()
    {
        rb.velocity = new Vector2 (horizontal*speed,rb.velocity.y);
        if(!facingrigth && horizontal > 0f)
        {
            Flip();
        }
        else if (facingrigth && horizontal < 0f)
        {
            Flip();
        }
    }  

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y*.5f);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
    private void Flip()
    {
        facingrigth = !facingrigth;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
