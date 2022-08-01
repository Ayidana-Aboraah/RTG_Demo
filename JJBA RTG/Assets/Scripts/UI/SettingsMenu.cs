using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using Cinemachine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public AudioMixer mixer;
    public GameObject Boss_bar; // TODO: Make a Minimaslist setting 
    public CinemachineFreeLook cam; // TODO: Make a camera sensetivity setting

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);

    public void SetVolume(float volume) => mixer.SetFloat("Volume", volume);

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        Screen.fullScreen = isFullScreen;
    }
    public void SetMinimalist(bool minimal) => Boss_bar.SetActive(minimal);
    
    public void SetSensitivity(float val) => cam.m_YAxis.m_MaxSpeed = val;

    void SwitchControls(int idx){}
}
