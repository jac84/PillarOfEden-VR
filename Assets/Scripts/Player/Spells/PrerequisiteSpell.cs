using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeapMotionSimpleControl;
using Leap;

public class PrerequisiteSpell : MonoBehaviour
{
    [SerializeField] protected GestureManager gestureManager;
    private void Awake()
    {
        gestureManager = transform.parent.GetComponent<GestureManager>();
    }
    public virtual bool checkPrerequisite(Hand hand)
    {
        return false;
    }
    public virtual bool brokePrerequisite(Hand hand)
    {
        return true;
    }
}