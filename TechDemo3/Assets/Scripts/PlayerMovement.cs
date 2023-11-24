using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float speed;
    public Animator animator; 
    [SerializeField] private ThumbStickController thumbStick;


    private Rigidbody2D rb;
    private Vector2 movement; 
    private SpriteRenderer sprite;
    
          
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movement = new Vector2(horizontalInput, verticalInput).normalized;

        if (thumbStick.TouchDir != Vector2.zero)
        {
            movement = thumbStick.TouchDir;
        }   
        
        SetSprite();
        FaceTarget(); 
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);        
    }

    void SetSprite()
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("Speed", movement.magnitude);
        
        if (movement != Vector2.zero)
            sprite.flipX = movement.x < 0;          
    }

    void FaceTarget()
    {
        SerpentController activeEnemy = GameManager.instance.currentTarget;

        if (activeEnemy != null)
        {
            animator.SetBool("isTargeting", true);
            animator.SetFloat("Vertical", activeEnemy.transform.position.y - transform.position.y);
        }
        else
        {
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("isTargeting", false);
        }

        Vector3 targetDirection = activeEnemy.transform.position - transform.position;
        
        if (targetDirection.x > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
        if(targetDirection.y > transform.position.y)
        {
           // spriteRenderer.sprite = spriteUp; 
        }
        else
        {
           // spriteRenderer.sprite = spriteDown; 
        }
    }
}
