using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Frappi Info", menuName = "Frappi/Frappi Info", order = 1)]
public class FrappiInfo : ScriptableObject
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
}
