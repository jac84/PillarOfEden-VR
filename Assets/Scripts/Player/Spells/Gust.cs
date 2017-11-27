using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : Spell
{ 
    [Header("Gust Attributes")]
    [SerializeField] private float AoEDuration;
    [SerializeField] private Vector3 ExapnsionVector;
    [SerializeField] private float ExapnsionRate;

    public override void ActivateSpell()
    {
        Spell spell = player.GetCurrentSpell();
        if (spell != null)
        {
            if (spell.GetType() == typeof(Gust))
            {
                if (!player.GetBeads().SpendMana(MPCost))
                    return;
                AoEGrow pa;
                if (GamManager.singleton.IsLeftHanded())
                {
                    pa = projectile.Instantiate<AoEGrow>();
                    pa.gameObject.transform.position = player.leftHandPosition.position;
                    pa.SetCenter(player.leftHandPosition);
                    pa.gameObject.transform.rotation = Quaternion.identity;
                }
                else
                {
                    pa = projectile.Instantiate<AoEGrow>();
                    pa.gameObject.transform.position = player.rightHandPosition.position;
                    pa.SetCenter(player.rightHandPosition);
                    pa.gameObject.transform.rotation = Quaternion.identity;
                }
                pa.SetDamage(damage);
                pa.SetDieTime(AoEDuration);
                pa.SetExapnsionVector(ExapnsionVector);
                pa.SetExpansionRate(ExapnsionRate);
                Debug.Log(pa.gameObject.transform.position);
            }
        }
    }

    public override void DeactivateSpell()
    {
    }
}
