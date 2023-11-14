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
    public float buffDuration = 30f;

   
    public AbilityResult ApplyArmorBuff()
    {
        return new AbilityResult(defenceMultiplier, manaRegenRate, buffDuration);
    }

    [System.Serializable]
    public struct AbilityResult
    {
        public float defenceMultiplier;
        public float manaRegenRate;
        public float buffDuration;

        public AbilityResult(float defenceMultiplier, float manaRegenRate, float buffDuration)
        {
            this.defenceMultiplier = defenceMultiplier;
            this.buffDuration = buffDuration;
            this.manaRegenRate = manaRegenRate;
        }

    }

   


}
