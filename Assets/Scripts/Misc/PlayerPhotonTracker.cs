using UnityEngine;
using System.Collections;

public class PlayerPhotonTracker : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = GamManager.singleton.player.transform.position;
    }
}
