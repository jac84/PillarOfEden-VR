/*******************************************************
 * Copyright (C) 2016 Ngan Do - dttngan91@gmail.com
 *******************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Leap;
using Leap.Unity;
using UnityEngine.UI;
using CustomUtils;

namespace LeapMotionSimpleControl
{
    public class GestureManager : MonoBehaviour
    {

        public enum GestureTypes
        {
            HandsOpenHold,
            SwipingLeft,
            HandsOpen,
            SwipingRight,
            SwipingUp,
            SwipingDown,
            ThumbUp,
            ThumbDown,
            Fist,
            FaceUp,
            FaceDown,
            ClapHand,
            Grab,
            Throw,
            RotateHonz,
            RotateVert,
            ZoomHand            
        }

        public GestureTypes _currentType;

        public GestureTypes GteCurrentGestureType()
        {
            return _currentType;
        }

        public LeapProvider _leapHandProvider;

        public LeapProvider GetLeapHand()
        {
            return _leapHandProvider;
        }

        public float TimeBetween2Gestures;

        public Dictionary<GestureTypes, object> _listActiveGestures;

        public Dictionary<GestureTypes, object> GetCurrentActiveGestures()
        {
            return _listActiveGestures;
        }

        public Transform player;

        // Use this for initialization
        void Start()
        {
            Invoke("initGesture", 3f);
        }

        public void initGesture()
        {
            _listActiveGestures = new Dictionary<GestureTypes, object>();
            foreach (Transform t in transform)
            {
                BehaviorHand hand = t.GetComponent<BehaviorHand>();
                if (hand != null)
                {
                    hand.SetPlayerTransform(player);
                    foreach (GestureTypes type in Enum.GetValues(typeof(GestureTypes)))
                    {
                        if (hand.GetCurrentType() == type && !_listActiveGestures.ContainsKey(type))
                        {
                            if (type == GestureTypes.RotateHonz || type == GestureTypes.RotateVert)
                            {
                                _listActiveGestures.Add(GestureTypes.RotateHonz, t.GetComponent<BehaviorHand>() as object);
                                _listActiveGestures.Add(GestureTypes.RotateVert, t.GetComponent<BehaviorHand>() as object);
                            }
                            else
                            {
                                _listActiveGestures.Add(type, t.GetComponent<BehaviorHand>() as object);
                            }
                        }
                    }
                    t.GetComponent<BehaviorHand>().Init(this);
                }
            }
        }
        public BehaviorHand ActiveBehaviorHand;
        public void ResetHandOnExit()
        {
            if (ActiveBehaviorHand != null)
            {
                ActiveBehaviorHand.GetCounter().DelayReset = false;
                ActiveBehaviorHand.ResetListHands();
                Debug.Log("ResetHand");
            }           
        }

        public virtual bool ReceiveEvent(GestureTypes type)
        {
            Debug.Log("ReceiveEvent " + type.ToString());
            _currentType = type;
            Invoke("unBlockCurrentGesture", TimeBetween2Gestures);
            return true;
        }

        public void unBlockCurrentGesture()
        {
            BehaviorHand behavior = (BehaviorHand)_listActiveGestures[_currentType];
            behavior.UnBlockGesture();
        }

        public void unBlockGesture(GestureTypes type)
        {
            BehaviorHand behavior = (BehaviorHand)_listActiveGestures[type];
            behavior.UnBlockGesture();
        }

        public virtual void LoadingGestureProgress(GestureTypes type, float percent)
        {

        }
        #region Logic Common
        public Vector3 getHandVelocity(Hand hand)
        {
            if (player == null)
                player = transform.root;
            return player.InverseTransformDirection(UnityVectorExtension.ToVector3(hand.PalmVelocity));
        }

        public bool isMoveLeft(Hand hand,float deltaVelocity,float smallestVelocity)
        {
            //Debug.Log (getHandVelocity (hand) + " - " + deltaVelocity);
            return getHandVelocity(hand).x < -deltaVelocity && !isStationary(hand, smallestVelocity);
        }

        public bool isMoveRight(Hand hand,float deltaVelocity,float smallestVelocity)
        {
            return getHandVelocity(hand).x > deltaVelocity && !isStationary(hand, smallestVelocity);
        }

        public bool isMoveUp(Hand hand,float deltaVelocity,float smallestVelocity)
        {
            return hand.PalmVelocity.y > deltaVelocity && !isStationary(hand, smallestVelocity);
        }

        public bool isMoveDown(Hand hand,float deltaVelocity, float smallestVelocity)
        {
            return hand.PalmVelocity.y < -deltaVelocity && !isStationary(hand, smallestVelocity);
        }

        public bool isStationary(Hand hand,float smallestVelocity)
        {
            return hand.PalmVelocity.Magnitude < smallestVelocity;
        }

        public bool isHandConfidence(Hand hand)
        {
            return hand.Confidence > 0.5f;
        }

        public bool isGrabHand(Hand hand)
        {
            return hand.GrabStrength > 0.8f;
        }

        public bool isCloseHand(Hand hand,float deltaCloseFinger)
        {
            List<Finger> listOfFingers = hand.Fingers;
            int count = 0;
            for (int f = 0; f < listOfFingers.Count; f++)
            {
                Finger finger = listOfFingers[f];
                if ((finger.TipPosition - hand.PalmPosition).Magnitude < deltaCloseFinger)
                {
                    count++;
                    //				if (finger.Type == Finger.FingerType.TYPE_THUMB)
                    //					Debug.Log ((finger.TipPosition - hand.PalmPosition).Magnitude);
                }
            }
            return (count == 5);
        }

        public bool isOpenFullHand(Hand hand)
        {
            //Debug.Log (hand.GrabStrength + " " + hand.PalmVelocity + " " + hand.PalmVelocity.Magnitude);
            return hand.GrabStrength == 0;
        }
        //<Jason> - Pillar of Eden
        public bool isAllFingersExtended(Hand hand)
        {
            int count = 0;
            List<Finger> fingers = hand.Fingers;
            foreach (Finger finger in fingers)
            {
                if (finger.IsExtended)
                {
                    count++;
                }
            }
            if (count == 5)
                return true;
            return false;
        }
        //<Jason> - Pillar Of Eden
        public bool isFingerExtended(Hand hand, Finger.FingerType type)
        {
            List<Finger> listOfFingers = hand.Fingers;
            for (int f = 0; f < listOfFingers.Count; f++)
            {
                Finger finger = listOfFingers[f];

                if (finger.Type == type)
                {
                    if(finger.IsExtended)
                    {
                        return true;
                    }
                }
            }
                    return false;
        }


        public bool isPalmNormalSameDirectionWith(Hand hand, Vector3 dir, float handForwardDegree)
        {
            return isSameDirection(hand.PalmNormal, UnityVectorExtension.ToVector(dir), handForwardDegree);
        }

        public bool isHandMoveForward(Hand hand,float handForwardDegree,float smallestVelocity)
        {
            return isSameDirection(hand.PalmNormal, hand.PalmVelocity, handForwardDegree) && !isStationary(hand, smallestVelocity);
        }

        #region ThumbUp/Down

        public bool checkPalmNormalInXZPlane(Hand hand)
        {
            float anglePalmNormal = angle2LeapVectors(hand.PalmNormal, UnityVectorExtension.ToVector(Vector3.up));

            return (anglePalmNormal > 70 && anglePalmNormal < 110);
        }

        // check thumb finger up/down
        public bool isThumbDirection(Hand hand, Vector3 dir,float deltaAngleThumb)
        {
            List<Finger> listOfFingers = hand.Fingers;
            for (int f = 0; f < listOfFingers.Count; f++)
            {
                Finger finger = listOfFingers[f];

                if (finger.Type == Finger.FingerType.TYPE_THUMB)
                {
                    float angleThumbFinger = angle2LeapVectors(finger.Direction,
                                                UnityVectorExtension.ToVector(dir));
                    float angleThumbFinger2 = angle2LeapVectors(
                                                 finger.StabilizedTipPosition - hand.PalmPosition, UnityVectorExtension.ToVector(dir));
                    //Debug.Log (angleThumbFinger + " " + angleThumbFinger2);
                    if (angleThumbFinger < deltaAngleThumb
                       || angleThumbFinger2 < deltaAngleThumb)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        //Jason <PillarOfEden>
        public bool isFingerDirection(Hand hand,Finger.FingerType finterType ,Vector3 dir, float deltaAngle)
        {
            List<Finger> listOfFingers = hand.Fingers;
            for (int f = 0; f < listOfFingers.Count; f++)
            {
                Finger finger = listOfFingers[f];

                if (finger.Type == finterType)
                {
                    float angleFinger = angle2LeapVectors(finger.Direction,
                                                UnityVectorExtension.ToVector(dir));
                    float angleFinger2 = angle2LeapVectors(
                                                 finger.StabilizedTipPosition - hand.PalmPosition, UnityVectorExtension.ToVector(dir));
                    //Debug.Log (angleThumbFinger + " " + angleThumbFinger2);
                    if (angleFinger < deltaAngle
                       || angleFinger2 < deltaAngle)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        // check 4 fingers tip close to palm position
        public bool checkFingerCloseToHand(Hand hand,float deltaCloseFinger)
        {
            List<Finger> listOfFingers = hand.Fingers;
            int count = 0;
            for (int f = 0; f < listOfFingers.Count; f++)
            {
                Finger finger = listOfFingers[f];
                if ((finger.TipPosition - hand.PalmPosition).Magnitude < deltaCloseFinger)
                {
                    if (finger.Type != Finger.FingerType.TYPE_THUMB)
                    {
                        count++;
                    }
                }
            }
            //Debug.Log(count);
            return (count == 4);
        }

        #endregion

        #endregion

        #region Addition

        public bool isOppositeDirection(Vector a, Vector b,float handForwardDegree)
        {
            return angle2LeapVectors(a, b) > (180 - handForwardDegree);
        }

        public bool isSameDirection(Vector a, Vector b,float handForwardDegree)
        {
            //Debug.Log (angle2LeapVectors (a, b) + " " + b);
            return angle2LeapVectors(a, b) < handForwardDegree;
        }

        public float angle2LeapVectors(Leap.Vector a, Leap.Vector b)
        {
            return Vector3.Angle(UnityVectorExtension.ToVector3(a), UnityVectorExtension.ToVector3(b));
        }

        #endregion


    }
}