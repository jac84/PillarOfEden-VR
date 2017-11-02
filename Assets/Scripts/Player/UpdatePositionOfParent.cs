using UnityEngine;
using System.Collections;

public class UpdatePositionOfParent : MonoBehaviour
{
    [SerializeField]
    private Transform p;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = p.TransformPoint(p.position);
    }
}
