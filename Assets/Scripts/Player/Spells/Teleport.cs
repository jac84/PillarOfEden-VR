using UnityEngine;
using System.Collections;
using VRTK;
public class Teleport : Spell
{
    [SerializeField] private VRTK.VRTK_Pointer pointer;
    private void Start()
    {
        pointer.OnActivationButtonPressed(CheckTeleport);
    }
    public void CheckTeleport(object sender, ControllerInteractionEventArgs e)
    {
        if (player.GetBeads().GetMana() <= MPCost)
            pointer.enableTeleport = false;
        else
            pointer.enableTeleport = true;
    }
    public override void ActivateSpell()
    {
        player.GetBeads().SpendMana(MPCost);
    }

    public override void DeactivateSpell()
    {
    }
}
