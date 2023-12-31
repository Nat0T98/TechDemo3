using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Arcane Missile")]
public class ArcaneMissile : ScriptableObject
{
    public float manaCost = 150f;
    public float basePowerPerMissile = 15f;
    public float missileInterval = 1.0f;
    public float criticalHitChance = 25f;
    public float brillianceBuffDuration = 10f;
    public float channellingCancelThreshold = 0.1f;
    public GameObject prefab;
}
