using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Serpent Info", menuName ="Serpent/SerpentInfo ", order = 1)]
public class SerpentInfo : ScriptableObject
{

    public float maxHealth;
    public float currentHealth;
    public float baseDamage;
    public float defenceMultiplier;
    public float rangedAttackSpeed;
    public float detectionRadius;
    public float aggroSpeed; 
    public Sprite icon; 
    
}
