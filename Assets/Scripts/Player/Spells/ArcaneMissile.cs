using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissile : Spell {

    [SerializeField] private float Arc;
    public override void ActivateSpell()
    {
        Spell spell = player.GetCurrentSpell();
        if (spell != null)
        {
            if (spell.GetType() == typeof(ArcaneMissile))
            {
                Debug.Log("ArcaneMissile spell casted...");
                player.GetBeads().SpendMana(MPCost);
                GameObject p,target;
                target = player.GetTarget();
                if (GamManager.singleton.IsLeftHanded())
                {
                    p = Instantiate(projectile, player.leftHandPosition.position, Quaternion.identity);
                }
                else
                {
                    p = Instantiate(projectile, player.rightHandPosition.position, Quaternion.identity);
                }
                if (!target)
                {
                    p.GetComponent<Rigidbody>().AddForce(player.spellDirection.forward * 5, ForceMode.Impulse);
                }
                else
                {
                    p.GetComponent<HomingArc>().SeekTarget(target);
                }
            }
        }
    }

    public override void DeactivateSpell()
    {

    }
}
