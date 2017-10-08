using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRPlayer : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private VRTK_BasicTeleport teleport;
    [SerializeField] private Spell currentSpell;
    [SerializeField] private PlayerHPMP playerHealth;
    public Spell GetCurrentSpell()
    {
      return currentSpell;
    }

    public void UpdatePlayer()
    {

    }
}
