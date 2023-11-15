using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Frost Lance")]
public class FrostLance : ScriptableObject
{
    public float manaCost = 45;
    public float castingTime = 0f;
    public float coolDown = 0.5f;
    public float basePower = 10;
    public float maxStackAmount = 5;
    public float slowAmountPerStack = 0.15f;
    public float stackingEffectDuration = 5f;
    public float tripleDamageActivation = 5;
}
