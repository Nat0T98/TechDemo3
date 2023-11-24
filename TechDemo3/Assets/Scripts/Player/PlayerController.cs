using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Ability References")]
    public Ability fireBall;
    public Ability Armor;
    public Ability Missile;
    public Ability FrostLance;
    public GameObject FireBallPrefab;
    public GameObject MissilePrefab;
    public GameObject FrostLancePrefab;
    public GameObject ArmorPrefab;
    
   
    [Header("Player References")]
    public PlayerStats playerStats;
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float meleeAttackSpeed;
    private float nextAttackTime = 0f;
    public float defenceMultiplier;
    bool isPlayerDead;
    //private GameObject currentEnemy;
    private EnemyController currentEnemy;
    public SpriteRenderer playerSpriteColour;
    public Animator animator;
    public Vector3 startingPos;
    private PlayerMovement PlayerMovement;
   
    [Header("HUD References")]
    public Button AutoAttackButton;
    public Image AutoAttackButtonImage;
    private bool BoolAutoAttackEnabled;
    public GameObject playerDamagePrefab;

    

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = FindFirstObjectByType<PlayerMovement>();
        startingPos = transform.position;
        initialisePlayer();
        Debug.Log("Abilities are: " + playerStats.abilities);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDetectionRadius(); 
        updateAutoAttackButton();
       

    }
    private void FixedUpdate()
    {
      
    }

    public void initialisePlayer()  ///initialises all player variables locally 
    {
        Debug.Log("Player Has " + (playerStats.maxHealth) + " Health");
        maxHealth = playerStats.maxHealth;
       currentHealth = playerStats.maxHealth; 
        maxMana = playerStats.maxMana;
        currentMana = playerStats.maxMana; 
        meleeAttackSpeed = playerStats.meleeAttackSpeed;
        defenceMultiplier = playerStats.defenceMultiplier;
        transform.position = startingPos; 
        animator.SetBool("isDead", false);
        isPlayerDead = false; 
        
    }
    public void ManaRegen()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerStats.detectionRadius);

    }

    void CheckDetectionRadius() ///checks for enemy in range 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerStats.detectionRadius);

        bool enemyInRange = false;

        

        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                enemyInRange = true;
                if(collider.gameObject == GameManager.instance.activeEnemy.gameObject)
                {
                    currentEnemy = collider.gameObject.GetComponent<EnemyController>();
                    
                }
                else
                {
                    break; ////check to make sure the in range enemy is my target 
                }
                
                
                
                

                AutoAttackButton.interactable = true; AutoAttackButton.enabled = true;
                
                //break;
            }
        }
        nextAttackTime += Time.deltaTime;
        if (enemyInRange && meleeAttackSpeed <= nextAttackTime)
        { 

            if(BoolAutoAttackEnabled) //// same damage every time? 
            {
                
                MeleeAttack();
                //animator.SetBool("isAutoAttacking", true);
               
               
                
              nextAttackTime = 0;
            }
           else if(!BoolAutoAttackEnabled)
            {
                animator.SetBool("isAutoAttacking", false);
            }
           
            //AutoAttack.interactable = true; 
           //need a way to un set the auto attack button 
        }
        else if(!enemyInRange)
        {
            //Debug.Log("Not attacking");
            animator.SetBool("isAutoAttacking",false);
            
            AutoAttackButtonImage.color = Color.red;
            
            AutoAttackButton.interactable = true; 
            AutoAttackButton.enabled = true;
            currentEnemy = null; 
        }
    }

    void updateAutoAttackButton()   ///performs functionality of auto attack button for various circumstances 
    {
        if (currentEnemy != null && BoolAutoAttackEnabled)
        {
            AutoAttackButtonImage.color = Color.white;
            AutoAttackButton.interactable = true;
            //ToggleAutoAttack();
        }
        else if(currentEnemy != null && !BoolAutoAttackEnabled) 
        {
            AutoAttackButtonImage.color = Color.red;
            AutoAttackButton.interactable = true; 
            //ToggleAutoAttack();
        }
        if(currentEnemy == null && !BoolAutoAttackEnabled)
        {
            
            AutoAttackButtonImage.color = Color.red;
            AutoAttackButton.interactable = true;
        }
        else if (currentEnemy == null && BoolAutoAttackEnabled)
        {
            AutoAttackButtonImage.color = Color.white;
            AutoAttackButton.interactable = true;
        }
    }

    public void ToggleAutoAttack()  //simply turning auto attack on and off 
    {
        Debug.Log("Pressing Toggle Button");
        
        BoolAutoAttackEnabled = !BoolAutoAttackEnabled;
        if(!BoolAutoAttackEnabled)
        {
            AutoAttackButtonImage.color = Color.red;
           // Debug.Log("auto attack disabled");
            currentEnemy = null; 
        }
        else
        {
            AutoAttackButtonImage.color = Color.white;
           // Debug.Log("Auto attack enabled"); 

        }
    }



    void MeleeAttack() /// normal attack function 
    {



        if (currentEnemy != null)
        {
            animator.SetBool("isAutoAttacking", true);
            Debug.Log("Attacking");
            DamageManager.DealEnemyDamage(currentEnemy, playerStats.baseDamage);

        }
        else
        {
            Debug.Log("Cant attack. No current enemy");
        }
    }

    public void TakeDamage(float damage)   
    {
        // damage -= currentHealth;
        float modifiedDamage = CalculateModifiedDamage(damage);
        currentHealth -= modifiedDamage;
        DamageManager.ShowDamage((int)modifiedDamage, playerDamagePrefab, transform);
        if(currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            isPlayerDead = true;
            StartCoroutine(DeathDelay()); 
            
            //PlayerMovement.enabled = false; 
            
        }
    }



    public float CalculateModifiedDamage(float baseDamage) //local take damage function. used in damage class to work out damage values
    {
        float minDamage = baseDamage * 0.75f;
        float maxDamage = baseDamage * 1.25f;
        float modifiedDamage = Random.Range(minDamage,maxDamage) * defenceMultiplier;
        return modifiedDamage;
    }

    public void FireBall()  ////broken when call the game manager load bar function 
    {

        currentEnemy = GameManager.instance.activeEnemy;
        if(currentMana >= fireBall.manaCost && currentEnemy != null)
        {

            
            currentMana -= fireBall.manaCost;
            GameManager.instance.LoadCastBar(fireBall.castingTime);
            if(AbilitiesManager.instance != null)
            {
              
                
                    AbilitiesManager.instance.UseFireball(transform.position, currentEnemy, FireBallPrefab, 5f);
                
               
            }
            else
            {
                Debug.Log("Abilities manager is null");
            }
            
            
           

        }
        else
        {
            Debug.Log("Not enough mana to perform FireBall/ Current Enemy Null");
        }
      
    
          
    
    }
   

    public bool IsPlayerDead() ///boolean to determine if the player is dead 
    {
        if(isPlayerDead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator DeathDelay() /// the 3 second delay to respawn the player 
    {
        yield return new WaitForSeconds(3f);
        //animator.SetBool("isDead", false); 
        //transform.position = startingPos;
        //isPlayerDead = false; 
        initialisePlayer(); 
    }

    

}
