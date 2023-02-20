using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public SettingMenu settingMenuUI;
    public SceneMove sceneManager;
    // Update is called once per frame

    private void Start() {
        settingMenuUI.Load();    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameisPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        if(settingMenuUI.gameObject.activeSelf){
            settingMenuUI.gameObject.SetActive(false);
            return;
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Reset(){
        Resume();
        sceneManager.Move("Battle");
    }

    public void Setting(){
        settingMenuUI.gameObject.SetActive(true);
    }

    public void Quiting(){
        Resume();
        sceneManager.Move("Lobby");
    }
}


