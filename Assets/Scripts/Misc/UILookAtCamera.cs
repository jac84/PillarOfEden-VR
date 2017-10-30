using UnityEngine;
using System.Collections;

public class UILookAtCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
            transform.LookAt(Camera.main.transform);
    }
}
