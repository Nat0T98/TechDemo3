using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SerpentController : MonoBehaviour
{
   public SerpentInfo SerpentInfo;
   public TextMeshProUGUI text; //for testing 
                                ////need reference to UI so can change current target  

    public float maxHealth;
    public float currentHealth;
    SerpentController currentActiveEnemy; 
      
    
    // Start is called before the first frame update
    void Start()
    {
        SetSerpentHealth();
    }

    // Update is called once per frame
    void Update()
    {      
        AggroRange(); 
    }

    private void SetSerpentHealth()
    {
        maxHealth = SerpentInfo.maxHealth;
        currentHealth = SerpentInfo.currentHealth;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SerpentInfo.detectionRadius);

    }

    void AggroRange()
    {
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(transform.position, SerpentInfo.detectionRadius);
        bool PlayerIsInRange = false;

        foreach (Collider2D col in Colliders)
        {
            if (col.CompareTag("Player"))
            {
                PlayerIsInRange = true;
                break;
            }
        }

        if (PlayerIsInRange == true)
        {
            Debug.Log("Can Attack Player");
        }
        else
        {
            Debug.Log("Player Not In Range");
        }
    }

}
