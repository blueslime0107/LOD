using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleCardManager : MonoBehaviour
{
    [SerializeField] bool playerCard;
    [SerializeField] Lobby lobby;
    public Stage stage;
    public TextMeshProUGUI title;
    public TextMeshProUGUI sub_text;
    public TextMeshProUGUI values;
    public CharItem[] characters = new CharItem[5];

    public void UpdateStat(){

        stage = (!playerCard) ? lobby.stage : lobby.stageManager.player_battleCard;

        title.text = stage.title;
        sub_text.text = stage.sub_text;
        values.text = stage.values;
        for(int i=0;i<stage.characters.Length;i++){
            if(stage.characters[i] != null){
                characters[i].gameObject.SetActive(true);
                characters[i].UpdateStat(stage.characters[i]);
            }
            else{
                characters[i].gameObject.SetActive(false);
            }

        }
    }
}
