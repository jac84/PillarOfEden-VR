using UnityEngine;
using System.Collections;

public class Smite : Spell
{
    public override void ActivateSpell()
    {
        Spell spell = player.GetCurrentSpell();
        if (spell != null)
        {
            if (spell.GetType() == typeof(Smite))
            {
                Debug.Log("Smite Casted...");
                player.GetBeads().SpendMana(MPCost);
                GameObject p;
                RaycastHit hit = GamManager.singleton.mainVRCamera.GetComponent<CameraRayCaster>().GetHit();
                if (hit.collider != null)
                {
                    p = Instantiate(projectile, hit.point, Quaternion.identity);
                }
            }
        }
    }

    public override void DeactivateSpell()
    {

    }
}
