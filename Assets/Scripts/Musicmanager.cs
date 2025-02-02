using UnityEngine;
using UnityEngine.UI;

public class Musicmanager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Slider volumeSlider;

    private static Musicmanager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load saved volume or set to default 0.5
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        backgroundMusic.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);  // Save volume setting
    }
}
