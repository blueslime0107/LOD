using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberIndi : MonoBehaviour
{
    public TextMeshPro tmPro;
    public Color resetColor;

    private void OnEnable() {
        tmPro.color = resetColor;
        StartCoroutine(MoveText());    
    }

    public IEnumerator MoveText(){
        float time = 0;
        Vector3 randVec;
        randVec = Vector3.right*Random.Range(-1f,2f);
        randVec += Vector3.up*Random.Range(-1f,2f);
        randVec += Vector3.forward*-12;
        while (true)
        {
            transform.Translate(randVec*0.5f*Time.deltaTime);
            transform.position.Set(transform.position.x,transform.position.y,-2);
            time += Time.deltaTime;
            if(time > 1f){
                gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }
}
