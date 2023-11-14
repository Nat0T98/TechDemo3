using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Info", menuName = "Player/Player Info", order = 1)]
public class PlayerInfo : ScriptableObject
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

    public List<GameObject> abilities;
    public List<GameObject> activeAbilities;   
    
}
