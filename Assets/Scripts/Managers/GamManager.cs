using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamManager : MonoBehaviour {
    
    [SerializeField] private PhoManager Network_Manager;

    // Use this for initialization
    void Start () {
        Network_Manager.AutoConnect("test");
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
