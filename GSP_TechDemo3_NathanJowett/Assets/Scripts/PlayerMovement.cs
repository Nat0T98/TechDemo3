using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     
   [SerializeField] private ThumbStickController ThumbStick;
   [SerializeField] float MoveSpeed;  

    private Rigidbody2D Player;    
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    private Vector2 PlayerMove; 
        
    
   
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
            anim.SetBool("isMoving", true);
            spriteRenderer.flipX = PlayerMove.x < 0; 
          
            if(Mathf.Abs(PlayerMove.y) > Mathf.Abs(PlayerMove.x))
            {
                anim.SetBool("isMovingVert", PlayerMove.y > 0);
                anim.SetBool("isMoving", PlayerMove.y <= 0); 
            }
            else
            {
                anim.SetBool("isMovingVert", false);
                anim.SetBool("isMoving", true); 
            }

        }
        else
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isMovingVert", false);
        }
    }
}
