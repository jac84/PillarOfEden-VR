using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour {

	public GameObject Projectile;
	public void FireProjectile()
	{
		GameObject p = Instantiate(Projectile,this.gameObject.transform.position,Quaternion.identity);
		p.GetComponent<Rigidbody>().AddForce(this.transform.forward * 5,ForceMode.Impulse);
	}
}
