using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Serpent HUD Info")]
    public GameObject SerpentHUD;
    public Image SerpentIcon;
    public Slider SerpentHealthSlider;
    public Slider SerpentManaSlider; 


    private SerpentController Serpent;  

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

    void Start()
    {
        
    }

    
    void Update()
    {
        UpdateSerpentHUD(); 
    }

    public void SetActiveEnemy(SerpentController Enemy)
    {
        if(Enemy != null)
        {
           Serpent = Enemy;
        }
        else
        {
            Serpent = null;
            SerpentIcon.sprite = null;
            SerpentHealthSlider.value = float.MinValue; 
        }
        
    }

    void UpdateSerpentHUD()
    {
       
        SerpentIcon.sprite = Serpent.SerpentInfo.serpentIcon;
        SerpentHealthSlider.value = Serpent.currentHealth; 
        
    }
}
