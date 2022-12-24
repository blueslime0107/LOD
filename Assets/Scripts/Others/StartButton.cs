using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button startButton;
    public Image teamColor;
    string teamname;

    Color32 blueColor = new Color32(0,0,255,255);
    Color32 redColor = new Color32(255,0,0,255);
    Color32 whiteColor = new Color32(255,255,255,255);

    public void updateYeamColor(string team){
        teamname = team;
        if(teamname.Equals("Right")){
            teamColor.color = redColor;
        }
        else{
            teamColor.color = blueColor;
        }
    }

    public IEnumerator startBlink(){
        while(true){
        teamColor.color = whiteColor;
        yield return new WaitForSeconds(0.4f);
        updateYeamColor(teamname);
        yield return new WaitForSeconds(0.4f);
        }
    }


}
