using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Slider slider;
    public TMP_Dropdown dropdown;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown vsyncDropdown;

    private Resolution[] resolutions;
    private int currentResolutionIndex;

    void Start()
    {
        // Initialize Volume
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            slider.value = Mathf.Pow(10, savedVolume / 20); // Convert back to linear
            audiomixer.SetFloat("MasterVolume", savedVolume);
        }
        slider.onValueChanged.AddListener(SetVolume);

        // Initialize Graphics Quality
        int qualityLevel = PlayerPrefs.GetInt("GraphicsQuality", 2); // Default medium quality
        dropdown.value = qualityLevel;
        QualitySettings.SetQualityLevel(qualityLevel);
        dropdown.onValueChanged.AddListener(SetGraphicsQuality);

        // Initialize Resolutions
        InitializeResolutionOptions();

        // Initialize V-Sync
        int vsyncSetting = PlayerPrefs.GetInt("VSync", 0); // Default to Off
        vsyncDropdown.value = vsyncSetting;
        QualitySettings.vSyncCount = vsyncSetting;
        vsyncDropdown.onValueChanged.AddListener(SetVSync);

        // Add listener for resolution dropdown changes
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void InitializeResolutionOptions()
    {
        resolutions = Screen.resolutions; // Get all available resolutions
        resolutionDropdown.ClearOptions(); // Clear existing options

        if (resolutions.Length == 0)
        {
            Debug.LogWarning("No resolutions found. Manually adding default resolutions.");
            resolutions = new Resolution[]
            {
                new Resolution { width = 1920, height = 1080 },
                new Resolution { width = 1280, height = 720 },
                new Resolution { width = 800, height = 600 }
            };
        }

        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionString = $"{resolutions[i].width} x {resolutions[i].height}";
            resolutionOptions.Add(resolutionString);
            Debug.Log($"Adding resolution: {resolutionString}");

            // Save current resolution index
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }   
        }

        resolutionDropdown.AddOptions(resolutionOptions); // Add options to the dropdown
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex); // Save selected resolution index
    }

    public void SetVSync(int vsyncSetting)
    {
        QualitySettings.vSyncCount = vsyncSetting; // 0 = Off, 1 = On
        PlayerPrefs.SetInt("VSync", vsyncSetting); // Save the V-Sync setting
    }
}
