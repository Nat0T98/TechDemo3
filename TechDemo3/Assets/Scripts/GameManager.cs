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
    public Slider castBarSlider;            
    public GameObject autoAttack;
    public bool CastBarFull; 
      
    [Header("Serpent Info")]
    public GameObject SerpentStats;
    public Image SerpentIcon;
    public Slider SerpentHealthSlider;
    public Image SerpentCurrentBuff;
    public SerpentController currentTarget;
   
    public List<SerpentController> ActiveSerpent;

    public static float hitChance = 80;
   


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
    }

    void Update()
    {
        UpdateFrappiPanel();
        updateSerpentPanel();
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
        if(currentTarget.isSerpentDead)
        {
            currentTarget = null;
            SerpentStats.SetActive(false);
            autoAttack.SetActive(false);

        }       

    }
    void UpdateFrappiPanel()
    {
        FrappiHealthSlider.value = Frappi.currentHealth; 
        FrappiManaSlider.value = Frappi.currentMana;    
    }

    
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


    //Damage Calculations
    public static bool HitChance(float hitChance)
    {
        float randomValue = Random.value * 100;

        return randomValue <= hitChance;
    }
    private static bool CritChance()
    {
        float critChance = 20;
        float critRand = Random.value * 100;

        if (critRand <= critChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    public static void SerpentDamager(SerpentController Target, float baseDamage)
    {
        if (HitChance(hitChance))
        {
            bool isCritical = CritChance();
            SerpentController Serpent = Target.GetComponent<SerpentController>();

            if (Serpent != null)
            {
                if (isCritical)
                {
                    baseDamage *= 2;
                }
                else
                {
                    float damage = Serpent.SDamageRange(baseDamage);
                    Serpent.TakeDamage(damage);
                    
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

    public static void FrappiDamager(GameObject target, float baseDamage)
    {
        if (HitChance(hitChance))
        {
            bool isCritical = CritChance();
            PlayerController Frappi = target.GetComponent<PlayerController>();
            if (Frappi != null)
            {
                if (isCritical)
                {
                    baseDamage *= 2;
                }
                else
                {
                    float Damage = Frappi.DamageRange(baseDamage);
                    Frappi.TakeDamage(Damage);                    
                }
            }
            else
            {
                Debug.LogError("Target Missing Player component");
            }
        }

    }

    public static void FloatingDamageNums(int damage, GameObject floatingNum, Transform transform)
    {
        Vector3 pos = new Vector3(0, 1, 0);
        var gObj = Instantiate(floatingNum, transform.position + pos, Quaternion.identity, transform);
        gObj.GetComponent<TextMesh>().text = damage.ToString();
        Destroy(gObj, 1f);
    }
       

}
