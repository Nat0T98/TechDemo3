using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInfo PlayerInfo;


    public FireBall FireBall;
    public FrostLance FrostLance;
    public ArcaneMissile ArcaneMissile;    
    public MageArmor MageArmor; 
    
    float currentHealth;
    float maxHealth;
    float minHealth;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {
        SerpentAggroRange(); 
    }

    private void SetPlayerHealth()
    {
        maxHealth = PlayerInfo.maxHealth;
        currentHealth = PlayerInfo.maxHealth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, PlayerInfo.detectionRadius);
    }

    void SerpentAggroRange()
    {
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(transform.position, PlayerInfo.detectionRadius);

        bool IsInRange = false; 

        foreach(Collider2D col in Colliders)
        {
            if(col.CompareTag("Serpent"))
            {
                IsInRange = true;
                break;
            }
        }

        if(IsInRange == true)
        {
            Debug.Log("Serpent in range");
        }
        else
        {
           Debug.Log("Serpent out of range");
        }
    }

  
}
