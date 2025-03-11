using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        LoadVolume(); // Cargar los valores guardados al iniciar

        // Asegurar que los sliders reflejen los valores actuales
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(delegate { SetSfxVolume(); });
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ApplySavedVolumes();
        }
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ApplySavedVolumes();
        }
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            musicSlider.value = musicVolume;
            audioMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            sfxSlider.value = sfxVolume;
            audioMixer.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);
        }
    }

    public void MuteToggle(bool isMuted)
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].mute = isMuted;
        }
    }
}