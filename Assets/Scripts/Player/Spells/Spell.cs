using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

	[SerializeField] protected Transform handPosition;
	[SerializeField] protected GameObject projectile;
	[SerializeField] protected VRPlayer player;
	
	public abstract void ActivateSpell();
}
