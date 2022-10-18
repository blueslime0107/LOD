using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLine : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Camera camera;
    public LineRenderer lr;

    Vector3 camOffset = new Vector3(0,0,1);

    void Start(){
        camera = Camera.main;
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            lr.enabled = true;
            startPos = camera.ScreenToWorldPoint(Input.mousePosition)+camOffset;
            lr.SetPosition(0, startPos);

        }
        if(Input.GetMouseButton(0)){
            endPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.SetPosition(1,endPos);
        }   
        if(Input.GetMouseButtonUp(0)){
            lr.enabled = false;
        }
    }
}
