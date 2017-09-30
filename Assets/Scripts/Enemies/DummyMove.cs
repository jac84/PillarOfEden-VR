using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMove : MonoBehaviour {

	[SerializeField] private Rigidbody rigidbody;
	[SerializeField] private float speed;
	private int direction = 1;

	// Use this for initialization
	public void Move()
	{
		if(rigidbody.position.x > 5)
		{
			direction = -1;
		}
		else if(rigidbody.position.x < -5)
		{
			direction = 1;
		}
		rigidbody.velocity = new Vector3(speed * direction,0.0f,0.0f)* Time.fixedDeltaTime;
	}
}
