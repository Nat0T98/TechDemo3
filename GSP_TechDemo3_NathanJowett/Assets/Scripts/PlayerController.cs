using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    public SerpentInfo SerpentInfo;
    private SerpentController serpentController;

    public FireBall FireBall;
    public FrostLance FrostLance;
    public ArcaneMissile ArcaneMissile;    
    public MageArmor MageArmor; 
    
    float currentHealth;
    float maxHealth;
    float minHealth;

    
    void Start()
    {
        SetPlayerHealth();
      
    }

    // Update is called once per frame
    void Update()
    {
        SerpentAggroRange();
        StartCoroutine(Death());
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
            
            Debug.Log("Serpent In Range");
            NormalAttack();
           
        }
        else
        {
          
           Debug.Log("Serpent Not In Range");
        }
    }


    void NormalAttack()
    {
       

    }

    void PlayerDeath()
    {
        SetPlayerHealth();
        //serpentController.
    }
    IEnumerator Death()
    {
        if(PlayerInfo.currentHealth <= 0) 
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(3);
            Instantiate(gameObject);
            SetPlayerHealth();
            //serpentController.SetSerpentHealth;
        }
    }
   
}
