using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Fire Ball")]
public class FireBall : Ability
{


    public float basePower = 35f;
    public float additionalDamage = 4f;
    public float additionalDamageInterval = 3f;
    public float debuffDuration = 15f;
    public float critMultiplier = 2f;
    




    public override void ExecuteAbility()
    {
        //executing Fireball 
    }
}
