using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/ Mage Armor")]
public class MageArmor : Ability
{
    public float defenceMultiplier = 0.65f;
    public float coolDown = 120f; 
    public float manaRegenRate = 25.0f;
    public float buffDuration = 30.0f; 

    public override void ExecuteAbility()
    {
        // execute armor 
    }
}
