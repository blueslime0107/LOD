using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAtk : MonoBehaviour
{
    public SoundManager sm;
    public List<SpriteRenderer> enemy_sprites = new List<SpriteRenderer>();
    public List<Character> characters = new List<Character>();
    public bool largeAtk;

    public void PlaySound(string text){
        sm.Play(text);
    }

    public void SetCondi(int condi){
        Debug.Log(condi);
        Debug.Log(characters[0]);
        foreach(SpriteRenderer sprite in enemy_sprites){
            sprite.gameObject.SetActive(false);
        }
        for( int i=0;i<characters.Count;i++){
            enemy_sprites[i].gameObject.SetActive(true);
            enemy_sprites[i].sprite = characters[i].char_sprites.poses[condi];
        }
    }


    
}
