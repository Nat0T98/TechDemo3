using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   public EnemyStats EnemyStats;
   public TextMeshProUGUI text; //for testing 
                                ////need reference to UI so can change current target  

    public float maxHealth;
    public float currentHealth;
    EnemyController currentActiveEnemy; 

    
    
    // Start is called before the first frame update
    void Start()
    {
        initialiseEnemy(); 
    }

    // Update is called once per frame
    void Update()
    {
      
        
        if(Input.touchCount==1  || Input.touchCount==2)
        {
           
                if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 touchPos = new Vector2(pos.x, pos.y);

                    var Hit = Physics2D.OverlapPoint(touchPos);
                    if (Hit)
                    {
                       EnemyController newTarget = Hit.transform.GetComponent<EnemyController>();
                    if (newTarget != null)
                    {

                        if (Hit.transform == transform)
                        {


                            text.GetComponent<TextMeshProUGUI>().text = "Tapped. Target Selected";
                            GameManager.instance.SetActiveEnemy(newTarget);
                            currentActiveEnemy = newTarget;

                            // change UI to reflect new target
                            // "gameobject is current target or smth like that 
                        }
                    }
                    else
                    {
                        // GameManager.instance.SetActiveEnemy(null);
                    }
                    }
                    else
                    {
                        text.GetComponent<TextMeshProUGUI>().text = "Not tapped. no target selected";
                       // GameManager.instance.SetActiveEnemy(null);
                    }
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended)
                {
                    text.GetComponent<TextMeshProUGUI>().text = "End of tap";
                }
            

        }
        
            


        
        CheckforPlayerInRange(); 
    }
    void initialiseEnemy()
    {
        Debug.Log("Enemy has " + (EnemyStats.maxHealth) + " Health");
         maxHealth = EnemyStats.maxHealth;
         currentHealth = EnemyStats.currentHealth;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyStats.detectionRadius);

    }


    void CheckforPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, EnemyStats.detectionRadius);

        bool PlayerInRange = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerInRange = true;
                break;
            }
        }
        if (PlayerInRange)
        {
            Debug.Log("Player in range. Attack Player");
        }
        else
        {
            Debug.Log("Player Out of range");
        }
    }

  void TakeDamage(int damage)
    {

    }
}
