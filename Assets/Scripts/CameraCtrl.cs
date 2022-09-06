using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    public BattleManager battleManager;

    bool isTwoTargetMove = false;
    public bool isZeroMove = false;
    Player ch1;
    Player ch2;
    Vector3 target1;
    Vector3 target2;
    float moveSpeed;

    float maxCamsize = 5f;
    float minCamsize = 3f;

    [SerializeField] [Range(0f, 10f)] private float speed = 1;
    [SerializeField] [Range(0f, 10f)] private float radius = 1;
    private float runningTime = 0;

    Camera camer;


    // Start is called before the first frame update
    void Start()
    {
        camer = GetComponent<Camera>();
        StartCoroutine("CameraZoom");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(isTwoTargetMove){
            target1 = ch1.transform.position;
            target2 = ch2.transform.position;
            Vector3 tr = (target1 + target2) * 0.5f + Vector3.back*10 + Vector3.down*0.5f;
            if(Vector3.Distance(transform.position,tr) > 0.001f){
                transform.position = Vector3.MoveTowards(transform.position,tr,moveSpeed*Time.deltaTime);
            }

            
        }
        else if(isZeroMove){
            if(Vector3.Distance(transform.position,Vector3.back*10) > 0.001f){
                transform.position = Vector3.MoveTowards(transform.position,Vector3.back*10,moveSpeed*Time.deltaTime);
            }
            else{
                transform.position = Vector3.back*10;
                isZeroMove = false;
            }
        }

    }

    IEnumerator CameraZoom(){
        while(true){
            if(isTwoTargetMove && target1 != null && target2 != null){
            float size = Vector3.Distance(target1,target2) / 14 * 5;
            if(size > maxCamsize){
                size = maxCamsize;
            }
            if(size < minCamsize){
                size = minCamsize;
            }
            camer.orthographicSize = size;
            }
            else{
                if(camer.orthographicSize < 5f){
                    camer.orthographicSize += 3f*Time.deltaTime;
                }
                else{
                    camer.orthographicSize = 5f;
                }
            }
        yield return null;
        }
        
    }

    public void SetTargetMove(Player tar1, Player tar2, float movSpd){
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
