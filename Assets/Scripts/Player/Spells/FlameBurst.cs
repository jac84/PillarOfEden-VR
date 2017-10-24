using UnityEngine;
using System.Collections;

public class FlameBurst : Spell
{
    private GameObject SpellObject;


    public override void ActivateSpell()
    {
        Spell spell = player.GetCurrentSpell();
        if (spell != null)
        {
            if (spell.GetType() == typeof(FlameBurst))
            {
                Debug.Log("FlameBurst Spell Casted...");
                player.GetBeads().SpendMana(MPCost);
                if (!SpellObject)
                {
                    if (GamManager.singleton.IsLeftHanded())
                    {
                        SpellObject = Instantiate(projectile, player.leftHandPosition.position, player.leftHandPosition.rotation);
                        SpellObject.transform.parent = player.leftHandPosition;
                    }
                    else
                    {
                        SpellObject = Instantiate(projectile, player.rightHandPosition.position, player.rightHandPosition.rotation);
                        SpellObject.transform.parent = player.rightHandPosition;
                    }
                }
            }
        }
    }

    public override void DeactivateSpell()
    {
        Destroy(SpellObject);
        SpellObject = null;
    }
}
