using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DitzelGames.FastIK;
using Unity.VisualScripting;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    [Tooltip("Can't be null")]
    public HandsIKContainer HandIK;
    public bool applyIK = false;

    [Space,Space,Tooltip("Can be null if don't need some IK elements. Contains local IK gameobject")]
    public ikPoint LeftHand_finger1;
    public ikPoint LeftHand_finger2;
    public ikPoint LeftHand_finger3;
    public ikPoint LeftHand_finger4;
    public ikPoint LeftHand_finger5;
    public ikPoint LeftHand;
    [Space]
    public ikPoint RightHand_finger1;
    public ikPoint RightHand_finger2;
    public ikPoint RightHand_finger3;
    public ikPoint RightHand_finger4;
    public ikPoint RightHand_finger5;
    public ikPoint RightHand;

    bool canIK = true;

    void Start() {
        if(LeftHand_finger1.Target == null && 
           LeftHand_finger2.Target == null &&
           LeftHand_finger3.Target == null &&
           LeftHand_finger4.Target == null &&
           LeftHand_finger5.Target == null &&
           LeftHand.Target == null &&
           RightHand_finger1.Target == null &&
           RightHand_finger2.Target == null &&
           RightHand_finger3.Target == null &&
           RightHand_finger4.Target == null &&
           RightHand_finger5.Target == null &&
           RightHand.Target == null) { Debug.Log("No <ikPoint> components found in children of ["+gameObject.name+"] for script <HandsManager>"); canIK = false; }
    }

    void Update() {
        if(canIK) {
            if(applyIK) {
                applyIK = false;
                applyIKToHands();
            }
        }
    }

    void applyIKToHands() {
        if(LeftHand_finger1.Target != null) { HandIK.LeftHand_finger1.Target = LeftHand_finger1.Target; HandIK.LeftHand_finger1.Pole = LeftHand_finger1.Pole; }
        if(LeftHand_finger2.Target != null) { HandIK.LeftHand_finger2.Target = LeftHand_finger2.Target; HandIK.LeftHand_finger2.Pole = LeftHand_finger2.Pole; }
        if(LeftHand_finger3.Target != null) { HandIK.LeftHand_finger3.Target = LeftHand_finger3.Target; HandIK.LeftHand_finger3.Pole = LeftHand_finger3.Pole; }
        if(LeftHand_finger4.Target != null) { HandIK.LeftHand_finger4.Target = LeftHand_finger4.Target; HandIK.LeftHand_finger4.Pole = LeftHand_finger4.Pole; }
        if(LeftHand_finger5.Target != null) { HandIK.LeftHand_finger5.Target = LeftHand_finger5.Target; HandIK.LeftHand_finger5.Pole = LeftHand_finger5.Pole; }
        
        if(LeftHand.Target != null) { HandIK.LeftHand.Target = LeftHand.Target; HandIK.LeftHand.Pole = LeftHand.Pole; }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if(RightHand_finger1.Target != null) { HandIK.RightHand_finger1.Target = RightHand_finger1.Target; HandIK.RightHand_finger1.Pole = RightHand_finger1.Pole; }
        if(RightHand_finger2.Target != null) { HandIK.RightHand_finger2.Target = RightHand_finger2.Target; HandIK.RightHand_finger2.Pole = RightHand_finger2.Pole; }
        if(RightHand_finger3.Target != null) { HandIK.RightHand_finger3.Target = RightHand_finger3.Target; HandIK.RightHand_finger3.Pole = RightHand_finger3.Pole; }
        if(RightHand_finger4.Target != null) { HandIK.RightHand_finger4.Target = RightHand_finger4.Target; HandIK.RightHand_finger4.Pole = RightHand_finger4.Pole; }
        if(RightHand_finger5.Target != null) { HandIK.RightHand_finger5.Target = RightHand_finger5.Target; HandIK.RightHand_finger5.Pole = RightHand_finger5.Pole; }
        
        if(RightHand.Target != null) { HandIK.RightHand.Target = RightHand.Target; HandIK.RightHand.Pole = RightHand.Pole; }
    }
    
    [Serializable]
    public class ikPoint {
        public Transform Target;
        public Transform Pole;
    }
}
