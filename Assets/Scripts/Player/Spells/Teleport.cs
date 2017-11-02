using UnityEngine;
using System.Collections;

public class Teleport : Spell
{
    public override void ActivateSpell()
    {
        player.GetBeads().SpendMana(MPCost);
    }

    public override void DeactivateSpell()
    {
    }
}
