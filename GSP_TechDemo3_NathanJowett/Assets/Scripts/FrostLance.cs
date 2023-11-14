using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Frost Lance")]
public class FrostLance : ScriptableObject
{
    public float manaCost = 45;
    public float castingTime = 0f;
    public float coolDown = 0.5f;
    public int basePower = 10;

    public int maxStacks = 5;
    public float slowEffectPerStack = 0.15f;
    public float stackingDuration = 5f;
    public float tripleDamageThreshold = 5;

    public int GetDamage(int CastCount)
    {
        if(CastCount ==  tripleDamageThreshold)
        {
            return basePower * 3;
        }
        return basePower; 

    }

}
