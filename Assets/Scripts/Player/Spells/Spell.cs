using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{

    [SerializeField] protected Transform handPosition;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected VRPlayer player;
    [SerializeField] protected int MPCost;

    public abstract void ActivateSpell();
}
