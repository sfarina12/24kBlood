using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [HideInInspector] public PlayerInputActionSet inputSet;

    void Awake() {
        inputSet = new PlayerInputActionSet();       
        inputSet.Enable();
    }

    void Update() {
        
    }
}
