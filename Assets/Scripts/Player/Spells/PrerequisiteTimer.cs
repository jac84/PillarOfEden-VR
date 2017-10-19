using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUtils;
using Leap;
using LeapMotionSimpleControl;

public class PrerequisiteTimer : Counter
{
    public bool prerequisiteMet = true;
    [SerializeField] private int maxTime;
    [SerializeField] private BehaviorHand behaviour;
    public bool checkPrerequisite()
    {
        if (!prerequisiteMet)
        {
            if (checkCondition())
            {
                Debug.Log("Timer Started...");
                prerequisiteMet = true;
                StartTimer(maxTime, () =>
                 {
                     Debug.Log("Timer Ended...");
                     prerequisiteMet = false;
                 });
            }
            else
            {
                if (CurrentState != CounterState.STOP)
                    StopTimer();
            }
        }
        return prerequisiteMet;
    }
    public void Reset()
    {
        Debug.Log("Timer Reset...");
        prerequisiteMet = false;
        StopTimer();   
    }
    public virtual bool checkCondition()
    {
        return true;
    }
    public virtual bool checkBrokePrerequisite()
    {
        return false;
    }
}
