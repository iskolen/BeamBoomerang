using TMPro;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public int bounceCount = 0;
    public int maxBounces = 5;
    public TextMeshProUGUI bounceText;
    public GameManager gameManager;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            bounceCount++;
            UpdateBounceText();

            if (bounceCount >= maxBounces)
            {
                gameManager.Lose();
            }
        }
        if (collision.gameObject.CompareTag("DangerWall"))
        {
            gameManager.Lose();
        }
    }
    void UpdateBounceText()
    {
        if (bounceText != null)
        {
            bounceText.text = "Отскоки: " + bounceCount + " / " + maxBounces;
        }
    }
}
