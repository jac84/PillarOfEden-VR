using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRPlayer : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private VRTK_BasicTeleport teleport;
    [SerializeField] private Spell currentSpell;
<<<<<<< HEAD
    [SerializeField] private PlayerHPMP playerBeads;
    [SerializeField] private VRTK_ControllerEvents controllerEvents;
=======
>>>>>>> b7b9f88c1211c5408fb64275625f9fed406fd918

    public Spell GetCurrentSpell()
    {
      return currentSpell;
    }
    public PlayerHPMP GetBeads()
    {
        return playerBeads;
    }

}
