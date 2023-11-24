using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public PlayerStats playerStats;
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float meleeAttackSpeed;
    private float nextAttackTime = 0f;
    public float defenceMultiplier;
    private bool isPlayerDead;
   
    private SerpentController activeTarget;
    public SpriteRenderer playerSpriteColour;
    public Animator animator;
    public Vector3 startingPos;
    private PlayerMovement PlayerMovement;
   
    [Header("HUD References")]
    public Button AutoAttackButton;
    public Image AutoAttackButtonImage;
    private bool IsAttackEnabled;
    public GameObject playerDamagePrefab;

    void Start()
    {
        PlayerMovement = FindFirstObjectByType<PlayerMovement>();
        startingPos = transform.position;
        SetPlayerStats();
    }

    void Update()
    {
        CheckAggroRange(); 
        isAutoAttackButton();       

    }
 
    public void SetPlayerStats() 
    {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerStats.detectionRadius);
    }

    void CheckAggroRange() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerStats.detectionRadius);
        bool SerpentAggro = false;
               

        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                SerpentAggro = true;
                if(collider.gameObject == GameManager.instance.currentTarget.gameObject)
                {
                    activeTarget = collider.gameObject.GetComponent<SerpentController>();
                    
                }
                else
                {
                    break; 
                }
               
                AutoAttackButton.interactable = true; AutoAttackButton.enabled = true;
                
            }
        }
        nextAttackTime += Time.deltaTime;
        if (SerpentAggro && meleeAttackSpeed <= nextAttackTime)
        { 

            if(IsAttackEnabled) 
            {                
                NormalAttack();          
                nextAttackTime = 0;
            }
            else if(!IsAttackEnabled)
            {
                animator.SetBool("isAutoAttacking", false);
            }          
            
        }
        else if(!SerpentAggro)
        {            
            animator.SetBool("isAutoAttacking",false);            
            AutoAttackButtonImage.color = Color.red;
            AutoAttackButton.interactable = true; 
            AutoAttackButton.enabled = true;
            activeTarget = null; 
        }
    }

    void isAutoAttackButton() 
    {
        if (activeTarget != null && IsAttackEnabled)
        {
            AutoAttackButtonImage.color = Color.green;
            AutoAttackButton.interactable = true;
        }
        else if(activeTarget != null && !IsAttackEnabled) 
        {
            AutoAttackButtonImage.color = Color.white;
            AutoAttackButton.interactable = true;             
        }
        
        if (activeTarget == null && IsAttackEnabled)
        {
            AutoAttackButtonImage.color = Color.green;
            AutoAttackButton.interactable = true;
        }
        else if(activeTarget == null && !IsAttackEnabled)
        {
            
            AutoAttackButtonImage.color = Color.white;
            AutoAttackButton.interactable = true;
        }
    }
       

    void NormalAttack() 
    {

        if (activeTarget != null)
        {
            animator.SetBool("isAutoAttacking", true);
            Debug.Log("Attacking");
            DamageManager.DealEnemyDamage(activeTarget, playerStats.baseDamage);
        }
        else
        {
            Debug.Log("Cant attack. No current enemy");
        }
    }

    public void TakeDamage(float damage)   
    {
        float modifiedDamage = CalculateModifiedDamage(damage);
        currentHealth -= modifiedDamage;
        DamageManager.ShowDamage((int)modifiedDamage, playerDamagePrefab, transform);

        if(currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            isPlayerDead = true;
            StartCoroutine(DeathDelay());                       
        }
    }

    public float CalculateModifiedDamage(float baseDamage) //local take damage function. used in damage class to work out damage values
    {
        float minDamage = baseDamage * 0.75f;
        float maxDamage = baseDamage * 1.25f;
        float modifiedDamage = Random.Range(minDamage,maxDamage) * defenceMultiplier;
        return modifiedDamage;
    }

    
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3f);
        SetPlayerStats(); 
    }

}
