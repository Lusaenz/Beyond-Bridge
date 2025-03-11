using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // Cargar valores de PlayerPrefs y asignarlos a los sliders
        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        float savedSfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.75f);

        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSfxVolume;

        // Escuchar cambios en los sliders
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);

        // Aplicar volúmenes al iniciar la escena
        AudioManager.Instance?.ApplySavedVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save(); // Guarda los cambios inmediatamente
        AudioManager.Instance?.ApplySavedVolumes(); // Aplicar el volumen en todas las escenas
    }

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();
        AudioManager.Instance?.ApplySavedVolumes();
    }

    public void MuteToggle(bool isMuted)
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].mute = isMuted;
        }
    }

    public void ButtonSFX()
    {
        AudioManager.Instance.PlaySFX("Button");
    }
}
