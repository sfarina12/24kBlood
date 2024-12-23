using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenAbjustItem : MonoBehaviour
{
    public bool is_left = false;
    [Range(0,1)]
    public float edgeDistance;
    public float cameraDistance;
    public float elementHeight;
    public Transform uiElement;
    public Camera camera;
    

    void Start() {  
    }

    void Update() {
        float distance = is_left ? - (edgeDistance / 2) : (edgeDistance / 2);
        Vector3 pos = camera.ViewportToWorldPoint(new Vector3(0.5f + distance,0,1)); 
        uiElement.position = pos;
        uiElement.localPosition = new Vector3(uiElement.localPosition.x,uiElement.localPosition.y + elementHeight,uiElement.localPosition.z + cameraDistance);  
    }
}
