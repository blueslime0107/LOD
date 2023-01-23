using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 전투카드를 불러와서 플레이어와 상대를 불러오는 스크립트
public class BattleCardManager : MonoBehaviour
{
    [SerializeField] bool playerCard;
    [SerializeField] Lobby lobby;
    public Stage stage;
    public TextMeshProUGUI title;
    public TextMeshProUGUI sub_text;
    public TextMeshProUGUI values;
    public List<CharItem> characters = new List<CharItem>();

    [SerializeField] Slider price_gague;
    [SerializeField] TextMeshProUGUI price_text;
    [SerializeField] GameObject overWritten;

    void Start(){
        foreach(CharItem chars in characters){
            chars.lobby = lobby;
        }
    }

    public void UpdateStat(){

        stage = (!playerCard) ? lobby.stage : lobby.stageManager.player_battleCard;

        

        title.text = stage.title;
        sub_text.text = stage.sub_text;
        values.text = stage.values;

        updatePriceGague();

        for(int i=0;i<stage.characters.Length;i++){
            if(stage.characters[i] != null){
                characters[i].gameObject.SetActive(true);
                characters[i].cur_stage = stage;
                characters[i].UpdateStat(stage.characters[i]);
            }
            else{
                characters[i].gameObject.SetActive(false);
            }

        }

        if(playerCard){

        List<CharItem> newCharList = characters.FindAll(x => x.cur_char != null);
        for(int i=0;i<newCharList.Count;i++){
            newCharList[i].cur_char.battleAble = true;
            if(i > lobby.stage.charlimit-1){
                newCharList[i].cur_char.battleAble = false;
            }
            newCharList[i].changeFightAble(newCharList[i].cur_char.battleAble);
        }
        lobby.battleButtonCharLimit();
        }

        
    }

    public void updatePriceGague(){
        price_gague.maxValue = stage.avaliblePrice;
        int getPrice = stage.GetPriceSum();
        price_gague.value = getPrice;
        price_text.text = price_gague.value.ToString() +"/"+price_gague.maxValue.ToString();
        overWritten.SetActive(getPrice > stage.avaliblePrice);
    }

    public void FightStart(){
        int playerLessFight = 0;
        foreach(CharItem chars in characters){
            if(chars.cur_char == null){break;}
            if(chars.cur_char.battleAble){playerLessFight++;}
        }
        if(playerLessFight > lobby.stage.charlimit || playerLessFight == 0){
            return;
        }
        if(stage.GetPriceSum() > stage.avaliblePrice){
            return;
        }
        lobby.GetStory();
    }
}
