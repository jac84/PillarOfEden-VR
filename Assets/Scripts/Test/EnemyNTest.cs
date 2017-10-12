using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNTest : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("l")){
            PhotonNetwork.Instantiate("Enemy", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0),0);
        }
	}
}
