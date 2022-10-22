using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BattleManager battleManager;
    public SceneMove sceneMove;
    public CameraCtrl main_camera_ctrl;

    void Start(){
        Application.targetFrameRate = 300;
        battleManager.Battle();
    }

    public Vector3 SetVector3z(Vector3 pre_vec,float z){
        Vector3 vec = pre_vec;
        vec.z = z;
        return vec;

    }

    
}
