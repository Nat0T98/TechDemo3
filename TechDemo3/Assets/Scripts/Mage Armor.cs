using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/ Mage Armor")]
public class MageArmor : ScriptableObject
{
    public float manaCost = 200f;
    public float defenceMultiplier = 0.65f;
    public float coolDown = 120f; 
    public float manaRegenRate = 25.0f;
    public float buffDuration = 30.0f;
    public GameObject prefab;
}
