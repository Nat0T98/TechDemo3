using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
  
    public static AbilityController instance;
    
    private GameObject activeFireBall;
    private GameObject activeMissile;

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindFirstObjectByType<AbilityController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
         
    }
    public void UseFireball(Vector3 spawnPos, SerpentController targetPos, GameObject fireBallPrefab, float speed)
    {
        Debug.Log("FireBall made from abilities manager");
        activeFireBall = Instantiate(fireBallPrefab, spawnPos, Quaternion.identity);

        Vector3 direction = (targetPos.transform.position - spawnPos).normalized;

        activeFireBall.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }


    public void UseMissile()
    {

    }

    public void UseFrostLance()
    {

    }
}
