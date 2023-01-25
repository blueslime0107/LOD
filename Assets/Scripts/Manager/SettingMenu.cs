using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public ProperContainer properContainer;
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public Slider sfxSlider;
    public Slider bgmSlider;
    public Toggle debugToggle;

    Resolution[] resolutions;

    void Awake(){
        properContainer = FindObjectOfType<ProperContainer>();
    }

    void Start(){
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();    

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i =0;i<resolutions.Length;i++){
            string option = resolutions[i].width+"x"+resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        sfxSlider.value = GetMixerLevel("SFX");
        bgmSlider.value = GetMixerLevel("BGM");
    }

    public float GetMixerLevel(string name){
         float value;
         bool result =  audioMixer.GetFloat(name, out value);
         if(result){
             return value;
         }else{
             return 0f;
         }
     }

    public void GoBack(){
        gameObject.SetActive(false);
    }

    public void SetSFXVolume(float volume){
        audioMixer.SetFloat("SFX",volume);
        if(volume == sfxSlider.minValue){
            audioMixer.SetFloat("SFX",-80);
        }
    }
    public void SetBGMVolume(float volume){
        audioMixer.SetFloat("BGM",volume);
        if(volume == bgmSlider.minValue){
            audioMixer.SetFloat("BGM",-80);
        }
    }
    public void SetDebugMode(bool boolen){
        properContainer.debugBoolen = boolen;
    }

    public void SetFullscreen(bool isFullscreed){
        Screen.fullScreen = isFullscreed;
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

}
