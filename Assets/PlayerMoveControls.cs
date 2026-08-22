using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private GatherInput gatherInput;
    private Rigidbody2D rigidbody2D;
    private int direction = 1;

    // Animation
    private Animator animator;
    public float raylength = 0.1f;
    public LayerMask groundLayer;
    public Transform leftpoint;
    public Transform rightpoint;
    private bool grounded = false;

    private void Awake()
    {
        gatherInput = GetComponent<GatherInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        CheckStatus();
        Move();
        Flip();
        setAnimationvalues();
        Jump();
       
        
    }

    private void CheckStatus()
    {
      
       grounded = Physics2D.Raycast(leftpoint.position, Vector2.down, raylength, groundLayer)
    ||
    Physics2D.Raycast(rightpoint.position, Vector2.down, raylength, groundLayer);


    }

    private void Move()
    {
        rigidbody2D.linearVelocity = new Vector2(speed * gatherInput.valueX, rigidbody2D.linearVelocity.y);
    }

    private void Flip()
    {
        if (gatherInput.valueX * direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x,1,1);
            direction *= -1;
        }
    }

    private void Jump()
    {
        
        if (gatherInput.jumpInput)
        {
            rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, jumpForce);
        }
        gatherInput.jumpInput = false;
    }

    private void setAnimationvalues()
    {
        animator.SetFloat("Speed", Mathf.Abs(gatherInput.valueX));
        animator.SetFloat("vSpeed", rigidbody2D.linearVelocity.y);
        animator.SetBool("Grounded", grounded);
    }
}