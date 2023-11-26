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
    public float baseDamage;
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
    public bool IsAttackEnabled;
    public GameObject FloatingNum;
    public static float hitChance = 80;

    void Start()
    {
        PlayerMovement = FindFirstObjectByType<PlayerMovement>();
        startingPos = transform.position;
        SetPlayerStats();
        isPlayerDead = false;
        IsAttackEnabled = true;
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
        baseDamage = FrappiInfo.baseDamage;
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
                //GameManager.SerpentDamager(activeTarget, FrappiInfo.baseDamage);

                AutoAttackDamage(activeTarget, baseDamage);       
                nextAttackTime = 0f;
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

    public void AutoAttackDamage(SerpentController serpent, float baseDamage)
    {
        if (GameManager.HitChance(hitChance))
        {
            bool isCritical = CritChance();
            activeTarget = GameManager.instance.currentTarget;

            if (activeTarget != null)
            {
                if (isCritical)
                {
                    baseDamage *= 2;
                }
                else
                {
                    float damage = activeTarget.SDamageRange(baseDamage);
                    activeTarget.TakeDamage(damage);

                }

            }

        }
        else
        {
            Debug.Log("Attack Missed");
        }
    }

    public bool HitChance(float hitChance)
    {
        float randomValue = Random.value * 100;

        return randomValue <= hitChance;
    }
    public bool CritChance()
    {
        float critChance = 20;
        float critRand = Random.value * 100;

        if (critRand <= critChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }





    public void ToggleAttack()
    {
        IsAttackEnabled = !IsAttackEnabled;
        Debug.Log("Attack toggled");
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
        Debug.Log("Player health is " + currentHealth);
        float randDamage = DamageRange(damage);
        currentHealth -= randDamage;
        GameManager.FloatingDamageNums((int)randDamage, FloatingNum, transform);
    }

    public float DamageRange(float baseDamage) 
    {
       
        float minDamage = baseDamage * 0.75f;
        float maxDamage = baseDamage * 1.25f;
        float randDamage = Random.Range(minDamage,maxDamage) * defenceMultiplier;
        if(activeTarget.currentHealth <= 50) 
        {
            return randDamage * 2;
        }
        else
        {
            return randDamage;
        }
        
    }

    
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3f);
        SetPlayerStats(); 
    }
    
                    

}
