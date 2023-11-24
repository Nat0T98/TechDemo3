using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] float speed;
    private Rigidbody2D rb;
    private Vector2 movement; 
    private SpriteRenderer spriteRenderer;
    public Animator animator;
     

    [SerializeField] private JoyStickScript thumbStick;
    ///public Sprite spriteUp;
    //public Sprite spriteDown; 
    
    
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


        //if (movement != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
           
        //}

        if (thumbStick.InputDirection != Vector2.zero)
        {
            movement = thumbStick.InputDirection;

        }
      
        
        
       updateSprite();
        FaceTarget(); 
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        //FaceTarget();
    }

    void updateSprite()
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("Speed", movement.magnitude);
        

        if (movement != Vector2.zero)
        {
            
            
            
            spriteRenderer.flipX = movement.x < 0;

            //  //spriteRenderer.sprite = (movement.y > 0) ? spriteUp : spriteDown;
            //    if(Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
            //    {
            //        animator.SetBool("isWalkingUp", movement.y > 0);
            //        animator.SetBool("isWalking", movement.y <= 0); 
            //    }
            //    else
            //    {
            //        animator.SetBool("isWalkingUp", false);
            //        animator.SetBool("isWalking", true); 
            //    }

            //}
            //else
            //{
            //    animator.SetBool("isWalking", false);
            //    animator.SetBool("isWalkingUp", false);
            //}
        }
    }

    void FaceTarget()
    {
        EnemyController activeEnemy = GameManager.instance.activeEnemy;

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
        //float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 

        if (targetDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
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
