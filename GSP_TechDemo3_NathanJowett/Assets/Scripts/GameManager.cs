using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public SerpentController SerpentController;
    public GameObject ActiveEnemy;

    private void Start()
    {
        ActiveEnemy = null;
    }
    private void Update()
    {

        setActiveEnemy();
        
    }


    public void setActiveEnemy()
    {
        //ActiveEnemy = SerpentController.gameObject;
    }

}
