using UnityEngine.UI;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public int bounceCount = 0;
    public int maxBounces = 5;
    public Text bounceText;
    public GameManager gameManager;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            bounceCount++;
            UpdateBounceText();

            AudioManager.Instance.PlayCollisionSound();

            if (bounceCount >= maxBounces)
            {
                gameManager.Lose();
            }
        }
        if (collision.gameObject.CompareTag("DangerWall"))
        {
            AudioManager.Instance.PlayCollisionSound();

            gameManager.Lose();
        }
    }
    void UpdateBounceText()
    {
        if (bounceText != null)
        {
            bounceText.text = "�������: " + bounceCount + " / " + maxBounces;
        }
    }
}
