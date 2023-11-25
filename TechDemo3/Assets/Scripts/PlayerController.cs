using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public FrappiInfo FrappiInfo;
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
   
    public Button AutoAttackButton;
    public Image AutoAttackButtonImage;
    private bool IsAttackEnabled;
    public GameObject FloatingNum;

    void Start()
    {
        PlayerMovement = FindFirstObjectByType<PlayerMovement>();
        startingPos = transform.position;
        SetPlayerStats();
        isPlayerDead = false;
    }

    void Update()
    {
        CheckAggroRange(); 
        isAutoAttackButton();

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            animator.SetBool("isDead", true);
            isPlayerDead = true;
            StartCoroutine(DeathDelay());
        }
        else
        {
            isPlayerDead = false;
        }
    }
 
    public void SetPlayerStats() 
    {
        maxHealth = FrappiInfo.maxHealth;
        currentHealth = FrappiInfo.maxHealth; 
        maxMana = FrappiInfo.maxMana;
        currentMana = FrappiInfo.maxMana; 
        meleeAttackSpeed = FrappiInfo.meleeAttackSpeed;
        defenceMultiplier = FrappiInfo.defenceMultiplier;
        transform.position = startingPos; 
        animator.SetBool("isDead", false);       
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FrappiInfo.detectionRadius);
    }

    public void CheckAggroRange() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, FrappiInfo.detectionRadius);
        bool inRange = false;
               

        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                inRange = true;
                if(collider.gameObject == GameManager.instance.gameObject)
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
        if (inRange == true && meleeAttackSpeed <= nextAttackTime)
        { 

            if(IsAttackEnabled) 
            {
                animator.SetBool("isAutoAttacking", true);
                GameManager.SerpentDamager(activeTarget, FrappiInfo.baseDamage);
                        
                nextAttackTime = 0;
            }
            else if(!IsAttackEnabled)
            {
                animator.SetBool("isAutoAttacking", false);
            }          
            
        }
        else if(inRange == false)
        {            
            animator.SetBool("isAutoAttacking",false);            
            AutoAttackButtonImage.color = Color.white;
            AutoAttackButton.interactable = true; 
            AutoAttackButton.enabled = true;
            activeTarget = null; 
        }
    }

    public void isAutoAttackButton() 
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
       
   
    public void TakeDamage(float damage)   
    {
        Debug.Log("current health is " + currentHealth);
        float randDamage = DamageRange(damage);
        currentHealth -= randDamage;
        GameManager.FloatingDamageNums((int)randDamage, FloatingNum, transform);
    }

    public float DamageRange(float baseDamage) 
    {
        float minDamage = baseDamage * 0.75f;
        float maxDamage = baseDamage * 1.25f;
        float randDamage = Random.Range(minDamage,maxDamage) * defenceMultiplier;
        return randDamage;
    }

    
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3f);
        SetPlayerStats(); 
    }

}
