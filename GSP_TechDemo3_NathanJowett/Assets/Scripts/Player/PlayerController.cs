using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;
    public FireBall fireBall;
    public ArcaneMissile ArcaneMissile;
    public FrostLance frostLance;
    public MageArmor MageArmor; 
    float maxHealth;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        initialisePlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDetectionRadius(); 
    }

    void initialisePlayer()
    {
        Debug.Log("Player Has " + (playerStats.maxHealth) + " Health");
        maxHealth = playerStats.maxHealth;
       currentHealth = playerStats.maxHealth; 
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerStats.detectionRadius);

    }

    void CheckDetectionRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, playerStats.detectionRadius);

        bool enemyInRange = false; 

        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Enemy"))
            {
                enemyInRange = true;
                break;
            }
        }
        if(enemyInRange)
        {
            //Debug.Log("Enemy in range");
        }
        else
        {
           //Debug.Log("Enemy Out of range");
        }
    }

  
}
