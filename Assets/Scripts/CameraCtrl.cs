using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    public BattleManager battleManager;

    bool isTwoTargetMove = false;
    bool isZeroMove = false;
    int ch1;
    int ch2;
    Vector3 target1;
    Vector3 target2;
    float moveSpeed;

    float maxCamsize = 5f;
    float minCamsize = 2f;

    Camera camer;


    // Start is called before the first frame update
    void Start()
    {
        camer = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(isTwoTargetMove){
            target1 = battleManager.players[ch1].transform.position;
            target2 = battleManager.players[ch2].transform.position;
            Vector3 tr = (target1 + target2) * 0.5f + Vector3.back*10;
            if(Vector3.Distance(transform.position,tr) > 0.001f){
                transform.position = Vector3.MoveTowards(transform.position,tr,moveSpeed*Time.deltaTime);
            }

            float size = Vector3.Distance(target1,target2) / 14 * 5;
            Debug.Log(size);
            if(size > maxCamsize){
                size = maxCamsize;
            }
            if(size < minCamsize){
                size = minCamsize;
            }
            camer.orthographicSize = size;
        }
        if(isZeroMove){
            if(Vector3.Distance(transform.position,Vector3.back*10) > 0.001f){
                transform.position = Vector3.MoveTowards(transform.position,Vector3.back*10,moveSpeed*Time.deltaTime);
            }
            else{
                transform.position = Vector3.back*10;
                isZeroMove = false;
                camer.orthographicSize = 5f;
            }
            if(camer.orthographicSize < 5f){
                camer.orthographicSize += 0.1f;
            }
        }
    }

    public void SetTargetMove(int tar1, int tar2, float movSpd){
        ch1 = tar1;
        ch2 = tar2;
        moveSpeed = movSpd;
        isZeroMove = false;
        isTwoTargetMove = true;
    }

    public void SetZeroMove(float movSpd){
        moveSpeed = movSpd;
        isTwoTargetMove = false;
        isZeroMove = true;
    }
}
