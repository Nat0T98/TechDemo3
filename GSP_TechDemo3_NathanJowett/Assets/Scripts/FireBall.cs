using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/FireBall")]
public class FireBall : ScriptableObject
{
    public float manaCost = 120;
    public float castingTime = 3f;
    public float basePower = 35;
    public float additionalDamage = 4;
    public float additionalDamageInterval = 3f;
    public float debuffDuration = 15f;
    public float critMultiplier = 2f; 
}
