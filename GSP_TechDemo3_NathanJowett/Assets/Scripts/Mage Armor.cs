using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Mage Armor")]
public class MageArmor : ScriptableObject
{
    public float manaCost = 200;
    public float CastingTime = 0f;
    public float coolDown = 120f;
    public float defenceMultiplier = 0.65f;
    public float manaRegenRate = 25f;
    public float regenDuration = 30f;

}
