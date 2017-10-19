/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CustomUtils;
using Leap;
using Leap.Unity;
using System;

namespace LeapMotionSimpleControl
{
    [RequireComponent(typeof(Counter))]
    public class BehaviorHand : MonoBehaviour
    {
        [SerializeField] protected bool _prerequisiteMet;

        Transform player;
        public void SetPlayerTransform(Transform t)
        {
            player = t;
        }
        protected GestureManager _gestureManager;
        protected bool _isBlock;

        public void UnBlockGesture()
        {
            _isBlock = false;
        }

        public GestureManager.GestureTypes CurrentType;
        public GestureManager.GestureTypes GetCurrentType()
        {
            return CurrentType;
        }

        protected Counter _counterLoading;
        List<Hand> _listHands;

        protected Hand GetCurrent1Hand()
        {
            if (_listHands.Count == 1)
                return _listHands[0];
            else
                return null;
        }

        protected List<Hand> GetCurrent2Hands()
        {
            if (_listHands.Count == 2)
                return _listHands;
            else
                return null;
        }

        #region Logic variables

        [Tooltip("Delta degree to check 2 vectors same direction")]
        protected float handForwardDegree = 30;

        [Tooltip("Grab hand strength, one for close hand(grab)")]
        protected float gradStrength = 0.9f;

        [Tooltip("Velocity (m/s) move toward ")]
        protected float smallestVelocity = 0.4f;

        [Tooltip("Velocity (m/s) move toward ")]
        protected float deltaVelocity = 1.0f;

        [Tooltip("Angle when rotation vector is registered to change")]
        protected float angleChangeRot = 4;

        [Tooltip("Opposite direction 2 vectors")]
        protected float diffAngle2Hands = 130;

        [Tooltip("Velocity opposite ")]
        protected float diffAngle2Velocity = 150;

        [Tooltip("Time (secs) during the user behavior checker")]
        public float CheckingTimeBeforeToggle = 1.5f;

        [SerializeField] protected float deltaCloseFinger = 0.05f;
        protected float deltaAngleThumb = 30;
        #endregion

        // Use this for initialization
        protected void Awake()
        {
            _counterLoading = GetComponent<Counter>();
            _isBlock = false;
            _prerequisiteMet = false;
        }

        // Update is called once per frame
        protected void FixedUpdate()
        {
            updateHands();
            updateDebug();
        }


        public void Init(GestureManager manager)
        {
            _gestureManager = manager;
        }

        void updateHands()
        {
            if (_gestureManager != null)
            {
                Frame frame = _gestureManager.GetLeapHand().CurrentFrame;
                _listHands = frame.Hands;
                if (!_isBlock)
                {
                    if (_listHands.Count > 0)
                    {
                        if (checkPrerequisite())
                        {
                            if (checkConditionGesture())
                            {
                                if (_counterLoading.CurrentState == Counter.CounterState.STOP)
                                {
                                    _counterLoading.StartTimerUpdatePercentage(CheckingTimeBeforeToggle, () =>
                                    {
                                        callEvent();
                                    }, (float percent) =>
                                    {
                                        if (CheckingTimeBeforeToggle != 0)
                                            _gestureManager.LoadingGestureProgress(CurrentType, percent);
                                    });
                                }
                            }
                        }
                        else
                        {
                            _counterLoading.StopTimer();
                            _gestureManager.LoadingGestureProgress(CurrentType, 0);
                        }
                    }
                }
            }
        }


        #region virtual methods

        protected virtual bool checkConditionGesture()
        {
            return false;
        }
        protected virtual bool hasPrerequisite()
        {
            return false;
        }
        protected virtual bool checkPrerequisite()
        {
            return false;
        }
        protected virtual void castSpell()
        {

        }

        protected Action specificEvent;

        protected void callEvent()
        {
            bool eventSuccess = _gestureManager.ReceiveEvent(CurrentType);
            if (eventSuccess)
            {
                _isBlock = true;
                castSpell();
                if (specificEvent != null)
                    specificEvent();
            }
        }

        #endregion

        #region Debug

        void updateDebug()
        {

            if (_listHands != null && _listHands.Count > 0 && _listHands.Count <= 2)
            {
                foreach (Hand hand in _listHands)
                {
                    Debug.DrawRay(UnityVectorExtension.ToVector3(hand.PalmPosition), UnityVectorExtension.ToVector3(hand.PalmNormal) * 10, Color.green);
                    if (!_gestureManager.isStationary(hand, smallestVelocity))
                    {
                        Debug.DrawRay(UnityVectorExtension.ToVector3(hand.PalmPosition), UnityVectorExtension.ToVector3(hand.PalmVelocity) * 10, Color.blue);

                    }
                }
            }
        }

        #endregion
    }
}