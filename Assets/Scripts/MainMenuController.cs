using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioClip mainMenuMusic;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(mainMenuMusic);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}
