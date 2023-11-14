using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] float MoveSpeed;
    private Rigidbody2D Player;
   [SerializeField] private ThumbStickController ThumbStick;
    private Vector2 PlayerMove; 
    private SpriteRenderer spriteRenderer;
    public Animator anim;

    public Sprite spriteUp;
    public Sprite spriteDown;    
    
   
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        PlayerMove = new Vector2(horizontalInput, verticalInput).normalized;

       if (ThumbStick.InputDirection != Vector2.zero)
       {
            PlayerMove = ThumbStick.InputDirection;
       }    
        
       PlayerMoveAnim(); 
    }

    private void FixedUpdate()
    {
        Player.MovePosition(Player.position + PlayerMove * MoveSpeed * Time.fixedDeltaTime); 
    }

    void PlayerMoveAnim()
    {
        if(PlayerMove != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            spriteRenderer.flipX = PlayerMove.x < 0; 
          
            if(Mathf.Abs(PlayerMove.y) > Mathf.Abs(PlayerMove.x))
            {
                anim.SetBool("isWalkingUp", PlayerMove.y > 0);
                anim.SetBool("isWalking", PlayerMove.y <= 0); 
            }
            else
            {
                anim.SetBool("isWalkingUp", false);
                anim.SetBool("isWalking", true); 
            }

        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalkingUp", false);
        }
    }
}
