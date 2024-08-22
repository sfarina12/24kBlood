using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class shooting : MonoBehaviour
{
    [Header("Input System"),Tooltip("Can be null. If null will get input system from player")]
    public PlayerInputManager input;
    [Space]
    public GameObject camera;
    public LayerMask ignoreLayers;
    [Min(1)]
    public float maxProjectileDistance = 10f;
    [Space]
    public Animator itemAnimator;
    public int numberOfAnimations;
    public string choiseVariableName;
    
    

    void Start() { 
        if(input == null) { 
            GameObject A = GameObject.Find("Player");
            if(A == null) { Debug.Log("No [Player] gameobject found for script <SC_FPSController>"); }
            input = A.GetComponent<PlayerInputManager>(); 
            if(input == null) { Debug.Log("No <PlayerInputManager> component found for script <SC_FPSController>"); }
        }
    }

    void Update() {
        bool isShooting = input.inputSet.Combat.Shoot.ReadValue<float>() == 1 ? true : false;
        bool isInteracting = itemAnimator.GetInteger("isInteracting") == 1 ? true : false;
        
        if(isShooting && !isInteracting) {
            int value = Random.Range(1, numberOfAnimations);
            itemAnimator.SetInteger(choiseVariableName, value);

            // RaycastHit hit;
            // if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxProjectileDistance, ~ignoreLayers)) {
            //     if(hit.collider.gameObject.tag == "hittable") {

            //     }
            // }
        }
    }
}
