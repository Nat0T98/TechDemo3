using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Arcane Missile")]
public class ArcaneMissile : Ability
{
    public float basePowerPerMissile = 15f;
    public float missileInterval = 1.0f;
    public float criticalHitChance = 25f;
    public float brillianceBuffDuration = 10f;
    public float channellingCancelThreshold = 0.1f; 




    public override void ExecuteAbility()
    {
       ///execute missile 
    }
}
