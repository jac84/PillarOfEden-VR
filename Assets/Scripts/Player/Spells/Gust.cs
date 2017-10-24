using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : Spell
{ 
    public override void ActivateSpell()
    {
        Spell spell = player.GetCurrentSpell();
        if (spell != null)
        {
            if (spell.GetType() == typeof(Gust))
            {
                Debug.Log("Gust Spell Casted...");
                player.GetBeads().SpendMana(MPCost);
                GameObject p;
                if (GamManager.singleton.IsLeftHanded())
                {
                    p = Instantiate(projectile, player.leftHandPosition.position, Quaternion.identity);
                }
                else
                {
                    p = Instantiate(projectile, player.rightHandPosition.position, Quaternion.identity);
                }
            }
        }
    }

    public override void DeactivateSpell()
    {
    }
}
