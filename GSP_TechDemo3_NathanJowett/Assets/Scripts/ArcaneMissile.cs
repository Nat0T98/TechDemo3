using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Arcane Missile")]
public class ArcaneMissile : ScriptableObject
{
    public float manaCost = 150;
    public float castingTime = 5f;
    public int basePowerPerMissile = 15;
    public float missileInterval = 1f;
    public int criticalHitChance = 25;
    public int brillianceBuffDuration = 10;
    public float channelingCancelThreshold = 0.1f; 

    public int GetTotalDamage()
    {
        int totalDamage = Mathf.CeilToInt(castingTime / missileInterval) * basePowerPerMissile;
        return totalDamage; 
    }

    public bool IsCriticalHit()
    {
        return Random.Range(0, 100) < criticalHitChance; 
    }
}
