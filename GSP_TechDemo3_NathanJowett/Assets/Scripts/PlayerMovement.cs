using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] float MoveSpeed;
    private Rigidbody2D rb;
   [SerializeField] private ThumbStickController ThumbStick;
    private Vector2 movement; 
    private SpriteRenderer spriteRenderer;
    public Animator animator;

    public Sprite spriteUp;
    public Sprite spriteDown; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movement = new Vector2(horizontalInput, verticalInput).normalized;

       if (ThumbStick.InputDirection != Vector2.zero)
       {
            movement = ThumbStick.InputDirection;
       }    
        
       PlayerMoveAnim(); 
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * MoveSpeed * Time.fixedDeltaTime); 
    }

    void PlayerMoveAnim()
    {
        if(movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            spriteRenderer.flipX = movement.x < 0; 

          //spriteRenderer.sprite = (movement.y > 0) ? spriteUp : spriteDown;
            if(Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
            {
                animator.SetBool("isWalkingUp", movement.y > 0);
                animator.SetBool("isWalking", movement.y <= 0); 
            }
            else
            {
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isWalking", true); 
            }

        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingUp", false);
        }
    }
}
