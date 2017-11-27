using UnityEngine;
using System.Collections;

public class FlameBurst : Spell
{
    private bool active;
    private Prototype pa;


    public override void ActivateSpell()
    {
        Spell spell = player.GetCurrentSpell();
        if (spell != null)
        {
            if (spell.GetType() == typeof(FlameBurst))
            {
                if (!player.GetBeads().SpendMana(MPCost))
                    return;
                if (!active)
                {
                    if (GamManager.singleton.IsLeftHanded())
                    {
                        pa = projectile.Instantiate<Prototype>();
                        pa.gameObject.transform.GetChild(0).GetComponent<Cone>().SetDamage(damage);
                        pa.gameObject.transform.position = player.leftHandPosition.position;
                        pa.gameObject.transform.parent = player.leftHandPosition;
                        pa.gameObject.transform.rotation = player.spellDirection.rotation;
                    }
                    else
                    {
                        pa = projectile.Instantiate<Prototype>();
                        pa.gameObject.transform.GetChild(0).GetComponent<Cone>().SetDamage(damage);
                        pa.gameObject.transform.position = player.rightHandPosition.position;
                        pa.gameObject.transform.parent = player.rightHandPosition;
                        pa.gameObject.transform.rotation = player.spellDirection.rotation;
                    }
                    
                    active = true;
                }
            }
        }
    }

    public override void DeactivateSpell()
    {
        if (active)
        {
            pa.ReturnToPool();
            active = false;
        }
    }
}
