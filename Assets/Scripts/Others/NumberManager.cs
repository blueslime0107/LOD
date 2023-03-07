using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NumberManager : MonoBehaviour
{
    List<NumberIndi> damList = new List<NumberIndi>();
    List<NumberIndi> healList = new List<NumberIndi>();

    [SerializeField]GameObject damPrefap; 
    [SerializeField]GameObject healPrefap; 

    private void Awake() {
        damList = new List<NumberIndi>(new NumberIndi[10]);

        for(int i=0;i<damList.Count;i++){
            damList[i] = Instantiate(damPrefap).GetComponent<NumberIndi>();
            damList[i].resetColor = damList[i].tmPro.color;
            damList[i].gameObject.SetActive(false);
        }

        healList = new List<NumberIndi>(new NumberIndi[10]);

        for(int i=0;i<healList.Count;i++){
            healList[i] = Instantiate(healPrefap).GetComponent<NumberIndi>();
            healList[i].resetColor = healList[i].tmPro.color;
            healList[i].gameObject.SetActive(false);
        }
    }

    public void IndicateDam(Transform trans, int num){
        for(int i=0;i<damList.Count;i++){
            if(!damList[i].gameObject.activeSelf){
            damList[i].transform.position = trans.position;
            damList[i].tmPro.text = "-" + num.ToString();
            damList[i].gameObject.SetActive(true);
            return;
            }
        }
    }

    public void IndicateHeal(Transform trans, int num){
        for(int i=0;i<healList.Count;i++){
            if(!healList[i].gameObject.activeSelf){
            healList[i].transform.position = trans.position;
            healList[i].tmPro.text = "+" + num.ToString();
            healList[i].gameObject.SetActive(true);
            return;
            }
        }
    }
}
