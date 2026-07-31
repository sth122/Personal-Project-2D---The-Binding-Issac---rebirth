using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISpriteAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField, Tooltip("애니메이션 프레임 배열")]
    private Sprite[] frames;

    [SerializeField, Tooltip("프레임당 교체 시간 (초)")]
    private float frameRate = 0.5f;

    private Image targetImage;
    private int currentFrame = 0;
    private float timer = 0f;

    private void Awake()
    {
        targetImage = GetComponent<Image>();
    }

    private void Start()
    {
        if (frames != null && frames.Length > 0)
        {
            targetImage.sprite = frames[0];
        }
    }

    private void Update()
    {
        if (frames == null || frames.Length <= 1) return;

        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame = (currentFrame + 1) % frames.Length;

            targetImage.sprite = frames[currentFrame];
        }
    }
}