using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	static GameManager instance = null;
	public Camera mainVRCamera;

	public static GameManager singleton
	{
		get{
			return instance;
		}
	}
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}else if(instance != this)
		{
			Destroy(gameObject);
		}
	}
}
