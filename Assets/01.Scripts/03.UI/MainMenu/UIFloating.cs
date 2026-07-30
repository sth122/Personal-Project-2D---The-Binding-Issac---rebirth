using UnityEngine;

public class UIFloating : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float speed;

    private RectTransform rectTransform;
    private float startY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;
    }
    private void Update()
    {
        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = startY + Mathf.Sin(Time.time * speed) * amplitude;
        rectTransform.anchoredPosition = pos;
    }
}
