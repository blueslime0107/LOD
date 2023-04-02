using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 전투카드를 불러와서 플레이어와 상대를 불러오는 스크립트
public class BattleCardManager : MonoBehaviour
{
    [SerializeField] CardExplain cardExplain;
    [SerializeField] bool playerCard;
    [SerializeField] Lobby lobby;
    public Stage stage;
    public List<CharItem> characters = new List<CharItem>();

    [SerializeField] Slider price_gague;
    [SerializeField] TextMeshProUGUI price_text;
    [SerializeField] GameObject overWritten;

    public int avaliblePrice;
    public List<CardAbility> NOTavalibleCard; 

    List<bool> disable_save = new List<bool>(); 

    void Start(){
        foreach(CharItem chars in characters){
            chars.lobby = lobby;
            foreach(CardUI cardUI in chars.cards){
                cardUI.cardExplain = cardExplain;
            }
            disable_save.Add(true);
        }
            disable_save.Add(false);

        
    }

    public void UpdateStat(){

        stage = (!playerCard) ? lobby.stage : lobby.stageManager.player_battleCard;

        stage.discovered = true;

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

        List<CharItem> newCharList = characters.FindAll(x => x.cur_char != null);

        
        for(int i=0;i<newCharList.Count;i++){
            if(disable_save[5]){
             newCharList[i].cur_char.battleAble = disable_save[i];   
            }
            else
            {newCharList[i].cur_char.battleAble = true;
            if(i > lobby.stage.charlimit-1){
                newCharList[i].cur_char.battleAble = false;
            }}
            newCharList[i].changeFightAble(newCharList[i].cur_char.battleAble);
        }

            price_gague.gameObject.SetActive(!stage.noCardEquipBreak);
            price_text.gameObject.SetActive(!stage.noCardEquipBreak);
         
        disable_save[5] = false;

        
    }

    public void SaveDisable(){
        List<CharItem> newCharList = characters.FindAll(x => x.cur_char != null);
        for(int i=0;i<newCharList.Count;i++){
            disable_save[i] = newCharList[i].cur_char.battleAble;
        }
        disable_save[5] = true;
    }

    public void updatePriceGague(){
        avaliblePrice = 0;
        NOTavalibleCard.Clear();
        foreach(Character character in stage.characters){
            if(character == null){break;}
            avaliblePrice += character.priceBonus;
            foreach(CardAbility card in character.char_preCards){
                if(card == null){break;}
                NOTavalibleCard.Add(card);
            }
        }
        lobby.menuCard.avaliblePrice = avaliblePrice;
        lobby.menuCard.NOTavalibleCard = NOTavalibleCard;
        price_gague.maxValue = avaliblePrice;
        int getPrice = stage.GetPriceSum();
        price_gague.value = getPrice;
        price_text.text = price_gague.value.ToString() +"/"+price_gague.maxValue.ToString();
        overWritten.SetActive(getPrice > avaliblePrice);
    }

    public void FightStart(){
        if(lobby.pc.debugBoolen){
            lobby.GetStory();
            return;
        }
        int playerLessFight = 0;
        foreach(CharItem chars in characters){
            if(chars.cur_char == null){break;}
            if(chars.cur_char.battleAble){playerLessFight++;}
        }
        if(playerLessFight > lobby.stage.charlimit || playerLessFight == 0){
            return;
        }
        if(stage.GetPriceSum() > avaliblePrice && !stage.noCardEquipBreak){
            return;
        }
        lobby.stageManager.saveManager.Save();
        lobby.GetStory();
    }
}
