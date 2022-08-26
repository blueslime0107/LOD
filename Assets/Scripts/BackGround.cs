using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // public GameObject BackA;
    // public GameObject BackB;
    // public GameObject BackC;
    // public GameObject BackD;

    // private float runningTime = 0;
    // [SerializeField] [Range(0f, 10f)] private float speed = 1;
    [SerializeField] [Range(0f, 90f)] private float rotate_spd = 1;

    // float self_Z;
    public BattleManager bm;
    public GameObject leftCircle;
    public GameObject rightCircle;
    // void Start(){
    //     self_Z = transform.position.z;
    // }

    void Update()
    {
        if(bm.left_turn){
            leftCircle.transform.Rotate(Vector3.forward * rotate_spd * Time.deltaTime);
        }
        if(bm.right_turn){
            rightCircle.transform.Rotate(Vector3.forward * rotate_spd * Time.deltaTime);
        }
    }

    public void TeamChanged(string team){
        if(team.Equals("Left")){
            leftCircle.SetActive(true);
            rightCircle.SetActive(false);
        }
        if(team.Equals("Right")){
            leftCircle.SetActive(false);
            rightCircle.SetActive(true);
        }
    }
}
