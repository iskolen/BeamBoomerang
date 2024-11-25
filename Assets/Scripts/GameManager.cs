using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject loseCanvas;

    void Start()
    {
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GeneralMenu");
    }

    public void Win()
    {
        winCanvas.SetActive(true);
        Time.timeScale = 0;

        AudioManager.Instance.PlayWinSound();
        AudioManager.Instance.AddSoundToAllButtons();
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Lose()
    {
        loseCanvas.SetActive(true);
        Time.timeScale = 0;

        AudioManager.Instance.PlayLoseSound();
        AudioManager.Instance.AddSoundToAllButtons();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
