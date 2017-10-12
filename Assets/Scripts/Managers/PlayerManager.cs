using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	[SerializeField] private VRPlayer vRPlayer;
	
	void Update()
	{
		vRPlayer.UpdatePlayer();
	}
}
