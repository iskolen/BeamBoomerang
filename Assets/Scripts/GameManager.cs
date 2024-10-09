using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winCanvas;

    void Start()
    {
        winCanvas.SetActive(false);
    }

    public void Win()
    {
        winCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
