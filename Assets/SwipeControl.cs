using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private Vector2 _startMousePosition, _endMousePosition;
    private Rigidbody2D _rb;
    public float MaxSwipeForce = 40f;
    public float MinSwipeDistance = 0.05f;
    public float SensitivityMultiplier = 2f;
    public LineRenderer lineRenderer;
    private bool isSwipeMade = false;
    private SpriteRenderer _spriteRenderer;

    // Для моргания
    public Color blinkColor = Color.red;
    public Color idleColor = Color.white;
    public Color swipeCompleteColor = Color.green;
    public float blinkSpeed = 2f; // Скорость моргания

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Получаем компонент SpriteRenderer
        _rb.bodyType = RigidbodyType2D.Static; // Замораживаем движение до свайпа
        lineRenderer.enabled = false; // Скрываем линию до начала свайпа
    }

    void Update()
    {
        // Если свайп не был сделан, продолжаем моргать
        if (!isSwipeMade)
        {
            BlinkEffect(); // Применяем эффект моргания
        }

        // Начало свайпа — включаем линию
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.enabled = true;  // Включаем линию для отрисовки
        }

        // В процессе свайпа обновляем линию
        if (Input.GetMouseButton(0))
        {
            _endMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawLine(_startMousePosition, _endMousePosition);  // Отрисовываем линию
        }

        // Завершение свайпа
        if (Input.GetMouseButtonUp(0))
        {
            _endMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Swipe();
            lineRenderer.enabled = false;  // Скрываем линию после свайпа
        }
    }

    // Метод для моргания перед первым свайпом
    void BlinkEffect()
    {
        // Меняем альфа-канал цвета для эффекта моргания
        float t = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
        _spriteRenderer.color = Color.Lerp(idleColor, blinkColor, t); // Плавное изменение между цветами
    }

    void Swipe()
    {
        Vector2 direction = _endMousePosition - _startMousePosition;
        float swipeDistance = direction.magnitude;

        if (swipeDistance < MinSwipeDistance)
        {
            return;
        }

        float swipeForce = Mathf.Clamp(swipeDistance * SensitivityMultiplier, 0, MaxSwipeForce);

        // После первого свайпа активируем физику и прекращаем моргание
        if (!isSwipeMade)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic; // Включаем физику
            isSwipeMade = true;
            _spriteRenderer.color = swipeCompleteColor; // Делаем мяч зеленым после свайпа
        }

        _rb.AddForce(-direction.normalized * swipeForce, ForceMode2D.Impulse);
    }

    // Отрисовка линии от точки старта к текущей позиции свайпа
    void DrawLine(Vector2 start, Vector2 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
