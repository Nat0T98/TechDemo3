using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    
    [Header("Frappi Info")]
    public GameObject FrappiStats; 
    public PlayerController Frappi;
    public Slider FrappiHealthSlider;
    public Slider FrappiManaSlider;   
    public Ability activeAbility; 
    public Slider castBarSlider;   
    public Image FireballButtonImage;     
    public GameObject autoAttack;
    public bool CastBarFull; 
    private bool isPlayerDead;
    public List<Ability> UIAbilities;


    [Header("Serpent Info")]
    public GameObject SerpentStats;
    public Image SerpentIcon;
    public Slider SerpentHealthSlider;
    public Image SerpentCurrentBuff;
    public SerpentController currentTarget;
   
    public List<SerpentController> enemies;
    
    private void Awake()
    {
        if (instance == null)
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
        SerpentStats.SetActive(false);
        autoAttack.SetActive(false);
        isPlayerDead= false;
    }

    void Update()
    {
        UpdateFrappiPanel();
        updateSerpentPanel();
        UpdateGame(); 
        
    }

    
    public void SetActiveTarget(SerpentController Target)
    {
        if (Target != null)
        {
            Debug.Log("Setting active enemy: " + Target.name);
            currentTarget = Target;
            
        }
        else
        {
            currentTarget = null;
            SerpentIcon.sprite = null;
            SerpentHealthSlider.value = float.MinValue;
        }
    }

    void updateSerpentPanel()
    {
        
        if (currentTarget != null)
        {
            // Debug.Log("Updating UI for active enemy: " + activeEnemy.name);
            SerpentStats.SetActive(true);
            SerpentIcon.sprite = currentTarget.SerpentInfo.icon;
            SerpentHealthSlider.value = currentTarget.currentHealth;
            autoAttack.SetActive(true);
        }
        else
        {
            SerpentStats.SetActive(false);
            autoAttack.SetActive(false); 
        }
        if(currentTarget.isEnemyDead)
        {
            currentTarget = null;
            SerpentStats.SetActive(false);
            autoAttack.SetActive(false);

        }

        ///add and remove abilities from player and enemy icons 
        ///for(all abilities in active abilities, add to sprite in abilities panel) 
        

    }
    void UpdateFrappiPanel()
    {
        FrappiHealthSlider.value = Frappi.currentHealth; 
        FrappiManaSlider.value = Frappi.currentMana;    
    }

    public void SetActiveCast(Ability ability)
    {
        activeAbility = ability;
        UIAbilities.Add(activeAbility);
        ///or add ability directly into the active ability list rather than assigning it to singular variable 

        //activeAbilityList.Add(activeAbility); ////smth like this? 
    }

    ///function to load cast bar and start particle effects HERE. needs to take casting time as parameter and return to ability function when done 
   public void LoadCastBar(float castTime)
    {
        Debug.Log("Loading Cast Bar");
        castBarSlider.value = 0;
        StartCoroutine(FillCastBar(castTime));
    }
    IEnumerator FillCastBar(float castTime)
    {
        CastBarFull = false;
        float elapsedTime = 0;
        castBarSlider.maxValue = castTime; 
        while (elapsedTime < castTime)
        {
            castBarSlider.value += elapsedTime / castTime;
            yield return null; 

            elapsedTime+= Time.deltaTime;           
            
        }
        
        castBarSlider.value = 0;
    }

    public bool isDead()
    {
        if(Frappi.IsPlayerDead())
        {
            return true;
            
        }
        else
        {
            return false;
        }

    }
    public void Respawn()
    {
        /// reset player and enemy values here 
        Debug.Log("Player has respawned");
    }
    public void UpdateGame() //resetting enemy pos and health when player dies 
    {
        if(isDead())
        {
           foreach(var enemy in enemies)
            {
                if(enemy.currentHealth > 0)
                {
                    enemy.SetSerpent();
                }
                else if(enemy.currentHealth <= 0)
                {

                }
            }
        }
    }
   
}
