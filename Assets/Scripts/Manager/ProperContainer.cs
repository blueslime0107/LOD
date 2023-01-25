using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProperContainer : MonoBehaviour
{
    SettingMenu settingMenu;
    public bool debugBoolen;

    private void Awake() {
        var obj = FindObjectsOfType<ProperContainer>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } 
    }
}
