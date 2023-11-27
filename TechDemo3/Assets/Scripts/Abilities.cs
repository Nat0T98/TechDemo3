using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Abilities : MonoBehaviour
{

    private PlayerController Frappi;
    private FireBall FireBall;
    private FrostLance FrostLance;
    private ArcaneMissile ArcaneMissile;
    private MageArmor MageArmor;

    public GameObject Fball;
    public GameObject FLance;
    public GameObject AMissile;
    public GameObject Armor;

    public void CastFireball()
    {
        Debug.Log("Casting Fireball");
       Instantiate(Fball, Frappi.transform.position, Quaternion.identity);
       Frappi.currentMana -= FireBall.manaCost;
       //Vector3 pos = new Vector3(2, 0, 0);
       
        
        if(Frappi.currentMana <= FireBall.manaCost)
        {
            Debug.Log("Not Enough Mana...");
        }

    }

    public void CastFrostLance()
    {
        Debug.Log("Casting Frost Lance");
        if (Frappi.currentMana <= FrostLance.manaCost)
        {
            Frappi.currentMana -= FrostLance.manaCost;
            Vector3 pos = new Vector3(2, 0, 0);
            var Obj = Instantiate(FLance, Frappi.transform.position + pos, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not Enough Mana...");
        }

    }

    public void CastArcaneMissile()
    {
        Debug.Log("Casting Arcane Missile");
        if (Frappi.currentMana <= ArcaneMissile.manaCost)
        {
            Frappi.currentMana -= ArcaneMissile.manaCost;
            Vector3 pos = new Vector3(2, 0, 0);
            var Obj = Instantiate(AMissile, Frappi.transform.position + pos, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not Enough Mana...");
        }

    }
    public void CastMageArmor()
    {
        Debug.Log("Casting Mage Armor");
        if (Frappi.currentMana <= MageArmor.manaCost)
        {
            Frappi.currentMana -= MageArmor.manaCost;
            Vector3 pos = new Vector3(2, 0, 0);
            var Obj = Instantiate(Armor, Frappi.transform.position + pos, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not Enough Mana...");
        }
        
        

    }
}
