using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossRoomManager : Singleton<BossRoomManager>
{
    [SerializeField] private RectTransform vsPanelRect;
    private float slideDruation = 1f;
    private float startX = 1700f;

    private bool isActive = false;

    protected override void Awake()
    {
        isDDOL = false;
        base.Awake();
    }


    private void Start()
    {
        if (vsPanelRect != null)
            vsPanelRect.gameObject.SetActive(false);
    }


    public void ShowBossIntro()
    {
        vsPanelRect.gameObject.SetActive(true);
        isActive = true;

        Time.timeScale = 0f;

        StartCoroutine(SlideInRoutine());
        SoundManager.Instance.PlayEnterTheBossRoomSFX();
    }

    private IEnumerator SlideInRoutine()
    {
        Vector2 startPos = new Vector2(startX, 0);
        Vector2 endPos = Vector2.zero;

        vsPanelRect.anchoredPosition = startPos;
        float time = 0f;
        while (time < slideDruation)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / slideDruation);
            float easeT = 1f - Mathf.Pow(1f - t, 3f);
            vsPanelRect.anchoredPosition = Vector2.Lerp(startPos, endPos, easeT);

            yield return null;
        }

        vsPanelRect.anchoredPosition = endPos;
    }

    private void Update()
    {
        if (!isActive)
            return;

        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SkipIntro();
        }
    }


    private void SkipIntro()
    {
        vsPanelRect.gameObject.SetActive(false);
        isActive = false;
        Time.timeScale = 1f;
        SoundManager.Instance.PlayBossRoomBGM();
    }
}
