using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class SerpentController : MonoBehaviour
{
   public SerpentInfo SerpentInfo;
   
    public enum SerpentState
    {
        Idle, 
        Aggro,
        Attacking,
        Dead
    }
    public SerpentState serpentState = SerpentState.Idle;
        
    private float maxHealth;
    public float currentHealth;
    private float defenceMultiplier;
    private float baseDamage;
    private float aggroSpeed;
    private float AttackTimer = 0f;
    private float timeBetweenAttacks;

    
    public Animator enemyAnim; 
    public SerpentController currentActiveEnemy;
    public GameObject floatingDamage;
    private Rigidbody2D rb;
    public Vector3 offset = new Vector3(0, 5, 0);
    private Vector3 EnemyStartingPos; 
    private SpriteRenderer sprite;
    public bool isSerpentDead; 

    private Transform playerTransform = null; 
     
     
    
    // Start is called before the first frame update
    void Start()
    {
        EnemyStartingPos = transform.position; 
        SetSerpent();
    }


    // Update is called once per frame
    void Update()
    {
        switch(serpentState)
        {
            case SerpentState.Idle:
                break;
            case SerpentState.Aggro:
                ChasePlayer();
                break;
            case SerpentState.Attacking:
                BaseAttack();
                break;
            case SerpentState.Dead:
                Die();
                break;

        }
        
        //Serpent Selection
        if(Input.touchCount > 0)  
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            { 
                if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                   return;
                }

                Vector3 pos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touch = new Vector2(pos.x, pos.y);

                int layermask = LayerMask.GetMask("Enemy", "Ground");
                RaycastHit2D Hit = Physics2D.Raycast(touch, Vector2.zero, Mathf.Infinity, layermask);
                Debug.Log("Hit Collider tag: " + Hit.collider.tag);

                   // var Hit = Physics2D.OverlapPoint(touchPos);
                if (Hit.collider != null)
                {
                    if(Hit.collider.CompareTag("Enemy"))
                    {
                       GameManager.instance.SetActiveTarget(Hit.collider.GetComponent<SerpentController>());
                       currentActiveEnemy = Hit.collider.GetComponent<SerpentController>();
                    }
                    else if(Hit.collider.CompareTag("Ground"))
                    {
                        
                        GameManager.instance.SetActiveTarget(null);
                        currentActiveEnemy = null; 
                    }
                   
                }
                else
                {
                    
                    // GameManager.instance.SetActiveEnemy(null);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                
            }          

        }             
        AgrroDetection(); 
    }

    public  void SetSerpent() 
    {
       
        maxHealth = SerpentInfo.maxHealth;
        currentHealth = SerpentInfo.currentHealth;
        defenceMultiplier = SerpentInfo.defenceMultiplier;
        baseDamage = SerpentInfo.baseDamage;
        aggroSpeed = SerpentInfo.aggroSpeed;
        timeBetweenAttacks = SerpentInfo.rangedAttackSpeed;
        transform.position = EnemyStartingPos; 
        sprite = GetComponent<SpriteRenderer>();
        serpentState = SerpentState.Idle;
        GameManager.instance.enemies.Add(this);
         

    }

    private void OnDrawGizmos() //to help check detection range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SerpentInfo.detectionRadius);

    }


    void AgrroDetection() 
    {        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, SerpentInfo.detectionRadius);
                     
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                
                playerTransform = collider.transform;
                if(serpentState != SerpentState.Dead)
                {
                    serpentState = SerpentState.Aggro;
                }              
                                     
             break;
            }
        }
        
    }


    public void MoveTowardsPlayer(Vector3 Frappi)
    {
        if(serpentState == SerpentState.Aggro)
        {
            Vector3 direction = (Frappi - transform.position).normalized;
            enemyAnim.SetFloat("Vertical", Frappi.y - transform.position.y);

            if (Vector3.Distance(Frappi, transform.position) > 4.0f)
            {
                if (Frappi.x > transform.position.x)
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

    }
    private void ChasePlayer()
    {
        if(playerTransform != null && serpentState != SerpentState.Dead)
        {
            AttackTimer += Time.deltaTime;
           
            if (AttackTimer >= timeBetweenAttacks)
            {
                serpentState = SerpentState.Attacking;
                AttackTimer = 0f;
            }
            enemyAnim.SetBool("isChasing", true);
            MoveTowardsPlayer(playerTransform.position);
                        
        }
    }
    private void BaseAttack()  //attacks player once then moves back to chase state 
    {
        if(playerTransform != null)
        {
            DamageManager.DealPlayerDamage(playerTransform.gameObject, baseDamage);            
            serpentState = SerpentState.Aggro;
            
        }
        else
        {
            Debug.Log("Cant attack player"); 
        }
    }
    public void TakeDamage(float damage)  //damage function, carries over to damage class where damage calcs take place 
    {
        float modifiedDamage = CalculateModifiedDamage(damage);
        currentHealth -= modifiedDamage;
        //Debug.Log(currentHealth);
        DamageManager.ShowDamage((int)modifiedDamage, floatingDamage, transform);  
        if(currentHealth <= 0)
        {            
            isSerpentDead = true;
            serpentState = SerpentState.Dead; 
        }
    }
    public float CalculateModifiedDamage(float baseDamage) //the local function for taking damage, used in damage class to work out the damage 
    {
        float minDamage = baseDamage * 0.75f;
        float maxDamage = baseDamage * 1.25f;
        float modifiedDamage = Random.Range(minDamage, maxDamage) * defenceMultiplier;
        return modifiedDamage; 
    } 

    private void Die()
    {        
        enemyAnim.SetBool("isChasing", false);
        enemyAnim.SetBool("isDead", true);        
    }
    


}
