using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip collisionSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        AddSoundToAllButtons();
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource.clip != musicClip)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }

    public void PlayCollisionSound()
    {
        PlaySFX(collisionSound);
    }

    public void PlayWinSound()
    {
        PlaySFX(winSound);
    }

    public void PlayLoseSound()
    {
        PlaySFX(loseSound);
    }

    private void AddSoundToAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            button.onClick.RemoveListener(PlayButtonClickSound);
            button.onClick.AddListener(PlayButtonClickSound);
        }
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            PlaySFX(buttonClickSound);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AddSoundToAllButtons();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
