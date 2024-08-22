using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;
using UnityEngine;

public class HandsIKContainer : MonoBehaviour
{
    [TextArea(1, 100), Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "[HOW DOES IT WORKS]\n" +
                          "Contains all the IK gameobjects of hands";

    public FastIKFabric LeftHand_finger1;
    public FastIKFabric LeftHand_finger2;
    public FastIKFabric LeftHand_finger3;
    public FastIKFabric LeftHand_finger4;
    public FastIKFabric LeftHand_finger5;
    public FastIKFabric LeftHand;
    [Space]
    public FastIKFabric RightHand_finger1;
    public FastIKFabric RightHand_finger2;
    public FastIKFabric RightHand_finger3;
    public FastIKFabric RightHand_finger4;
    public FastIKFabric RightHand_finger5;
    public FastIKFabric RightHand;
}
