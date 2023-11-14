using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject enemyUIPanel;
    public Image enemyIcon;
    public Slider enemyHealthSlider;
    public Slider enemyManaSlider; 


    private EnemyController activeEnemy;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateEnemyUI(); 
    }
    public void SetActiveEnemy(EnemyController newEnemy)
    {
        if(newEnemy != null)
        {
            activeEnemy = newEnemy;
        }
        else
        {
            activeEnemy = null;
            enemyIcon.sprite = null;
            enemyHealthSlider.value = float.MinValue; 
        }
        
    }

    void updateEnemyUI()
    {
       // enemyUIPanel.SetActive(true);

        enemyIcon.sprite = activeEnemy.EnemyStats.icon;
        enemyHealthSlider.value = activeEnemy.currentHealth; 
        
    }
}
