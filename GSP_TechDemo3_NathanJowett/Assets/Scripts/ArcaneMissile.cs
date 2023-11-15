using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Arcane Missile")]
public class ArcaneMissile : ScriptableObject
{
    public float manaCost = 150;
    public float castingTime = 5f;
    public float basePower = 15;
    public float missileDelay = 1f;
    public float critChance = 25;
    public float brillianceBuffDuration = 10;
    public float cancelEffect = 0.1f; 

}
