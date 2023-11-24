using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static float hitChance = 80;
   
    
    public static void DealEnemyDamage(EnemyController target, float baseDamage)
    {
        if (IsHit(hitChance))
        {
            bool isCritical = IsCriticalHit();
            EnemyController enemy = target.GetComponent<EnemyController>();

            if (enemy != null)
            {
                if(isCritical)
                {
                    baseDamage *= 2;
                    //Debug.Log("critical hit taken. damage of: " + baseDamage);
                }
                else
                {
                   // Debug.Log("Not critical damage");
                    float modifiedDamage = enemy.CalculateModifiedDamage(baseDamage);
                    enemy.TakeDamage(modifiedDamage);
                   // Debug.Log("Enemy Taken damage of: " + modifiedDamage);
                }
               
            }
            else
            {
                Debug.LogError("Target Missing Enemy controller component");
            }

        }
        else
        {
            Debug.Log("Attack Missed");
        }
    }

    public static void DealPlayerDamage(GameObject target, float baseDamage)
    {
       if(IsHit(hitChance))
        {
            bool isCritical = IsCriticalHit();
            PlayerController player = target.GetComponent<PlayerController>();
            if (player != null)
            {
                if(isCritical)
                {
                    baseDamage *= 2;
                }
                else
                {
                    float modifiedDamage = player.CalculateModifiedDamage(baseDamage);
                    player.TakeDamage(modifiedDamage);
                    //Debug.Log("Player Taken damage of: " + modifiedDamage); 
                }

            }
            else
            {
                Debug.LogError("Target Missing Player component");
            }
        }
        

        
    }

    public static void ShowDamage(int damage, GameObject damagePrefab, Transform transform)
    {
        Vector3 offset = new Vector3(0, 2, 0); 
        var go = Instantiate(damagePrefab, transform.position + offset, Quaternion.identity, transform);
       // Debug.Log("Instantiating Damage prefab: " + " pos: " + transform.position + offset);
        go.GetComponent<TextMesh>().text = damage.ToString();
        Destroy(go, 1f); 
    }

    public static bool IsHit(float hitChance)
    {
        float randomValue = Random.value * 100;

        return randomValue <= hitChance; 
    }
    private static bool IsCriticalHit()
    {
        float criticalChance = 20;
        float randomCritical = Random.value * 100;
        //Debug.Log("Random Critical Value: " + randomCritical);

       if(randomCritical <= criticalChance)
        {
            return true;
        }
        else
        {
            return false; 
        }
         
            
    }
   


}
