using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Fire Ball")]
public class FireBall : ScriptableObject
{
    public float manaCost = 120f;
    public float basePower = 35f;
    public float additionalDamage = 4f;
    public float additionalDamageInterval = 3f;
    public float debuffDuration = 15f;
    public float critMultiplier = 2f;
    public GameObject prefab; 
}
