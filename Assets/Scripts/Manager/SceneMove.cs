using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void MoveLobby(){
        SceneManager.LoadScene("Lobby");
    }

    public void MoveStory(){
        SceneManager.LoadScene("Talk");
    }

    public void MoveBattle(){
        SceneManager.LoadScene("Battle");
    }
}
