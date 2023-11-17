using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SerpentController : MonoBehaviour
{
   public SerpentInfo SerpentInfo;
  
    PlayerController playerController;
    public float maxHealth;
    public float currentHealth;
    SerpentController activeEnemy; 
      
    
    // Start is called before the first frame update
    void Start()
    {
        SetSerpentHealth();
    }

    // Update is called once per frame
    void Update()
    {      
        AggroRange();


        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touch = new Vector2(pos.x, pos.y);


                int layermask = LayerMask.GetMask("Serpent", "Ground");
                RaycastHit2D Hit = Physics2D.Raycast(touch, Vector2.zero, Mathf.Infinity, layermask);

                if (Hit.collider != null)
                {
                    if (Hit.collider.CompareTag("Serpent"))
                    {
                        activeEnemy = Hit.collider.GetComponent<SerpentController>();
                        Debug.Log("Active ememy is " + activeEnemy.gameObject);
                    }
                    else if (Hit.collider.CompareTag("Ground"));
                    {
                        Debug.Log("Enemy Deselected");                       
                        activeEnemy = null;
                    }
                    
                }
                else
                {
                    //activeEnemy = null;
                   
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                

            }

        }

        
    }

    void SetSerpentHealth()
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
            //AcidAttack();
        }
        else
        {
            Debug.Log("Player Not In Range");
        }
    }

}
