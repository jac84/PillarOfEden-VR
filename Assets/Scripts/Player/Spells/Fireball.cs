using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{

    public override void ActivateSpell()
    {
        Spell spell = player.GetCurrentSpell();
        if (spell != null)
        {
            if (spell.GetType() == typeof(Fireball))
            {
                GameObject p = Instantiate(projectile, handPosition.position, Quaternion.identity);
                p.GetComponent<Rigidbody>().AddForce(handPosition.forward * 5, ForceMode.Impulse);
            }
        }
    }
}
