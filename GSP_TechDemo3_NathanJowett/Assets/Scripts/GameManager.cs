using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public SerpentController SerpentController;
    private GameObject ActiveEnemy;
    
    private void Update()
    {
        
    }
    public void setActiveEnemy()
    {
        ActiveEnemy = null;
        if (ActiveEnemy != null)
        {
            ActiveEnemy = SerpentController.gameObject;
        }
       
    }


}
