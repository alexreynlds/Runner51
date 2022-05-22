using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class optionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resoDropdown;

    Resolution[] resolutions;

    // Gathering resolutions for the resolution setting
    void Start(){
        resolutions = Screen.resolutions;

        resoDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);
            
            // if(resolutions[i].width == Screen.currentResolution.width && 
            //    resolutions[i].height == Screen.currentResolution.height){
            //     currentResIndex = i;
            // }
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height){
                currentResIndex = i;
            }
        }

        resoDropdown.AddOptions(options);
        resoDropdown.value = currentResIndex;
        resoDropdown.RefreshShownValue();
    }

    public void setReso(int resoIndex){
        Resolution resolution = resolutions[resoIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setVolume(float volume){
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void setQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
}
