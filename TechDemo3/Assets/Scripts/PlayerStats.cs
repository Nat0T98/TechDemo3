using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player/Player Stats", order = 1)]
public class PlayerStats : ScriptableObject
{

    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public float meleeAttackSpeed;
    public float baseDamage;
    public float defenceMultiplier;
    public float manaRegen;
    public float detectionRadius; 
    public List<Ability> abilities;
    public List<Ability> activeAbilities; 
    
    
    
}
