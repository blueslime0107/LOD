using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLine : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Camera camera_;
    public LineRenderer lr;

    Vector3 camOffset = new Vector3(0,0,1);

    void Start(){
        camera_ = Camera.main;
    }

    void Update(){
        if(Input.GetMouseButton(0)){
            endPos = camera_.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.SetPosition(1,endPos);
        }   
    }
}
