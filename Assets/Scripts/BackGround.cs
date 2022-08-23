using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject BackA;
    public GameObject BackB;
    public GameObject BackC;
    public GameObject BackD;

    private float runningTime = 0;
    [SerializeField] [Range(0f, 10f)] private float speed = 1;
    [SerializeField] [Range(0f, 10f)] private float radius = 1;

    float self_Z;

    void Start(){
        self_Z = transform.position.z;
    }

    void Update()
    {
        runningTime += Time.deltaTime * speed;
        float x = radius * Mathf.Cos(runningTime);
        Debug.Log(x);
        //BackB.transform.Translate(Vector3.right*x*Time.deltaTime);
        // transform.position = Vector3.Lerp(transform.position,Vector3.back*10 + Vector3.right*x,2f*Time.deltaTime);
        BackA.transform.position = Vector3.right*x*0.5f + Vector3.forward*BackA.transform.position.z + Vector3.up;
        BackB.transform.position = Vector3.right*x + Vector3.forward*BackB.transform.position.z + Vector3.up;
        BackC.transform.position = Vector3.right*x*2f + Vector3.forward*BackC.transform.position.z + Vector3.up;
        BackD.transform.position = Vector3.right*x*3f + Vector3.forward*BackD.transform.position.z + Vector3.up;
    }
}
