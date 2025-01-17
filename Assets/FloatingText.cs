using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento hacia arriba
    public float fadeDuration = 1f; // Duración del desvanecimiento

    private CanvasGroup canvasGroup;
    private TextMeshProUGUI textComponent;
    private Vector3 initialPosition;
    private float lifetime;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(string message, Color textColor)
    {
        textComponent.text = message;
        textComponent.color = textColor;
        initialPosition = transform.position;
        lifetime = 0f;
    }

    void Update()
    {
        lifetime += Time.deltaTime;

        // Mover hacia arriba
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Desvanecer
        if (lifetime >= fadeDuration)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            if (canvasGroup.alpha <= 0f)
            {
                Destroy(gameObject); // Destruir el objeto cuando se desvanezca completamente
            }
        }
    }
}