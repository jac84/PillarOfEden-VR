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
                prerequisiteMet = true;
                StartTimer(maxTime, () =>
                 {
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
