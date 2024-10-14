using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public float MaxSwipeForce = 40f;
    public float MinSwipeDistance = 0.05f;
    public float SensitivityMultiplier = 2f;
    private bool isSwipeMade = false;

    public Color idleColor = Color.white;
    public Color swipeCompleteColor = Color.green;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb.bodyType = RigidbodyType2D.Static;
    }

    public bool IsMoving()
    {
        // Проверка, движется ли мяч
        return _rb.velocity.magnitude > 0.1f;
    }

    public void Swipe(Vector2 startMousePosition, Vector2 endMousePosition)
    {
        if (IsMoving()) return; // Если мяч уже движется, свайп игнорируется

        Vector2 direction = endMousePosition - startMousePosition;
        float swipeDistance = direction.magnitude;

        if (swipeDistance < MinSwipeDistance)
        {
            return;
        }

        float swipeForce = Mathf.Clamp(swipeDistance * SensitivityMultiplier, 0, MaxSwipeForce);

        if (!isSwipeMade)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            isSwipeMade = true;
            _spriteRenderer.color = swipeCompleteColor;
        }

        _rb.AddForce(-direction.normalized * swipeForce, ForceMode2D.Impulse);
    }
}
