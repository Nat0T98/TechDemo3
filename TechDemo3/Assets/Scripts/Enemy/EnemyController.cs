using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class EnemyController : MonoBehaviour
{
   public EnemyStats EnemyStats;
   public TextMeshProUGUI text; //for testing 
                                ////need reference to UI so can change current target  
                                ///
    
    public enum EnemyState
    {
        Idle, 
        Chase,
        Attack,
        Dead
    }
    public EnemyState enemyState = EnemyState.Idle;
    

    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float defenceMultiplier;
    public float baseDamage;
    public float aggroSpeed;
    private float AttackTimer = 0f;
    private float timeBetweenAttacks;

    [Header("Enemy References")]
    public Animator enemyAnim; 
    public EnemyController currentActiveEnemy;
    public GameObject floatingDamage;
    private Rigidbody2D rb;
    public Vector3 offset = new Vector3(0, 5, 0);
    private Vector3 EnemyStartingPos; 
    private SpriteRenderer sprite;
    public bool isEnemyDead; 

    private Transform playerTransform = null; 
     
     
    
    // Start is called before the first frame update
    void Start()
    {
        EnemyStartingPos = transform.position; 
        initialiseEnemy();
    }


    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break;
            case EnemyState.Dead:
                Die();
                break;

        }
        
        
        if(Input.touchCount > 0) ///swap to input.get mouse button 
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            { 
                    if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    return;
                }
                
                
                
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 touchPos = new Vector2(pos.x, pos.y);

                int layermask = LayerMask.GetMask("Enemy", "Ground");

                RaycastHit2D Hit = Physics2D.Raycast(touchPos, Vector2.zero, Mathf.Infinity, layermask);

                Debug.Log("Hit Collider tag: " + Hit.collider.tag);

                   // var Hit = Physics2D.OverlapPoint(touchPos);
                if (Hit.collider != null)
                {
                    if(Hit.collider.CompareTag("Enemy"))
                    {
                        text.GetComponent<TextMeshProUGUI>().text = "Tapped. Target selected";
                        GameManager.instance.SetActiveEnemy(Hit.collider.GetComponent<EnemyController>());
                        currentActiveEnemy = Hit.collider.GetComponent<EnemyController>();
                    }
                    else if(Hit.collider.CompareTag("Ground"))
                    {
                        text.GetComponent<TextMeshProUGUI>().text = "Tapped On ground. Target Deselected";
                        GameManager.instance.SetActiveEnemy(null);
                        currentActiveEnemy = null; 
                    }
                   
                }
                else
                {
                    text.GetComponent<TextMeshProUGUI>().text = "Not tapped. no target selected";
                    // GameManager.instance.SetActiveEnemy(null);
                }
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "End of tap";
                    
                   

                    
                }
            

        }
        
            


        
        CheckforPlayerInRange(); 
    }

   public  void initialiseEnemy()   ///initialises local values of the enemy 
    {
       // Debug.Log("Enemy has " + (EnemyStats.maxHealth) + " Health");
         maxHealth = EnemyStats.maxHealth;
         currentHealth = EnemyStats.currentHealth;
        defenceMultiplier = EnemyStats.defenceMultiplier;
        baseDamage = EnemyStats.baseDamage;
        aggroSpeed = EnemyStats.aggroSpeed;
        timeBetweenAttacks = EnemyStats.rangedAttackSpeed;
        //rb = GetComponent<Rigidbody2D>(); 
        transform.position = EnemyStartingPos; 
        sprite = GetComponent<SpriteRenderer>();
        enemyState = EnemyState.Idle;
        GameManager.instance.enemies.Add(this);
        transform.position = EnemyStartingPos; 

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyStats.detectionRadius);

    }


    void CheckforPlayerInRange()   ///constant check for if player is in range 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, EnemyStats.detectionRadius);

        bool PlayerInRange = false;
        //Transform playerTransform = null;
        
        

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerInRange = true;
                playerTransform = collider.transform;
                if(enemyState != EnemyState.Dead)
                {
                    enemyState = EnemyState.Chase;
                }
                
                    //DamageManager.DealPlayerDamage(collider.gameObject, baseDamage);
                    
             break;
            }
        }
        if (PlayerInRange)
        {
           
             //MoveTowardsPlayer(playerTransform.position);   ////setting aggro 
            // Debug.Log("Player in range. Attack Player");
            //aggro triggered, lock onto player 

        }
        else
        {
           
            //Debug.Log("Player Out of range");
            //playerTransform = null;
            //PlayerInRange = false; 
        }
    }

  public void TakeDamage(float damage)  ///damage function, carries over to damage class where damage calcs take place 
  {
        
        float modifiedDamage = CalculateModifiedDamage(damage);
        currentHealth -= modifiedDamage;
        //Debug.Log(currentHealth);
        DamageManager.ShowDamage((int)modifiedDamage, floatingDamage, transform);  
        if(currentHealth <= 0)
        {
            ///die animation and freeze. unable to respawn 
            isEnemyDead = true;
            enemyState = EnemyState.Dead; 
        }
  }


    public void MoveTowardsPlayer(Vector3 playerPos)  /// moves towards player during chase state 
    {
        if(enemyState == EnemyState.Chase)
        {
            Vector3 direction = (playerPos - transform.position).normalized;
            enemyAnim.SetFloat("Vertical", playerPos.y - transform.position.y);

            if (Vector3.Distance(playerPos, transform.position) > 4.0f)
            {
                if (playerPos.x > transform.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
                transform.Translate(direction * aggroSpeed * Time.deltaTime);

            }

        }




        //rb.velocity = new Vector3(direction.x * aggroSpeed, rb.velocity.y);

    }

   
   public float CalculateModifiedDamage(float baseDamage) ///the local function for taking damage, used in damage class to work out the damage 
    {
        float minDamage = baseDamage * 0.75f;
        float maxDamage = baseDamage * 1.25f;
        float modifiedDamage = Random.Range(minDamage, maxDamage) * defenceMultiplier;
        return modifiedDamage; 
    }

    private void ChasePlayer()  ///the chase state. moves to attack state when time condition is met 
    {
        if(playerTransform != null && enemyState != EnemyState.Dead)
        {
            AttackTimer += Time.deltaTime;
            //Debug.Log(AttackTimer);
            if (AttackTimer >= timeBetweenAttacks)
            {
                enemyState = EnemyState.Attack;
                // Debug.Log("Timer Reset and moving to attack phase");
                AttackTimer = 0f;
            }

            enemyAnim.SetBool("isChasing", true);
            MoveTowardsPlayer(playerTransform.position);
            
            
        }
    }
    private void AttackPlayer()  //attacks player once then moves back to chase state 
    {
        if(playerTransform != null)
        {
            DamageManager.DealPlayerDamage(playerTransform.gameObject, baseDamage);
            //Debug.Log("Attemped Attack on Player");
            enemyState = EnemyState.Chase;
            //Debug.Log("Moved back to chase phase"); 
        }
        else
        {
            Debug.Log("Cant attack player"); 
        }
    }

    private void Die()
    {
        ///freeze death animation on last frame 
        enemyAnim.SetBool("isChasing", false);
        enemyAnim.SetBool("isDead", true); 
        ///set dead animation to true 
    }
    


}
