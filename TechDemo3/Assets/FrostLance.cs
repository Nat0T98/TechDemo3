using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Frost Lance")]
public class FrostLance : Ability
{

    public float basePower = 10f;
    public float coolDown = 0.5f;
    public int MaxStacks = 5;
    public float slowEffectPerStack = 0.15f;
    public float stackingDuration = 5.0f;
    public float tripleDamageThreat = 5.0f; 



    public override void ExecuteAbility()
    {
        ///executing frost lance 
    }
}
