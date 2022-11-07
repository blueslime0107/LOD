using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainStage : MonoBehaviour
{
    public Lobby lobby;
    public string pack_name;
    public List<Stage> stage = new List<Stage>();
    public int rank;
    public Image rank_img;
    public Sprite[] rank_sprite;
    public TextMeshProUGUI battle_title;

    public void UpdateStat(){
        rank_img.sprite = rank_sprite[rank-1];
        battle_title.text = pack_name;
    }

    // public void Clicked(){
        
    // }
}
