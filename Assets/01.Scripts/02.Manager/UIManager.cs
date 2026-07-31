using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Canvas globalFadeCanvas;
    [SerializeField] private Image globalFadeImage;
    [SerializeField] private Image iconImage;

    public IEnumerator FadeOutRoutuine(Color targetColor, float fadeDuration)
    {
        globalFadeCanvas.gameObject.SetActive(true);
        globalFadeImage.raycastTarget = true;
        if (targetColor == Color.black)
            iconImage.gameObject.SetActive(true);

        Color color = targetColor;
        color.a = 0f;
        globalFadeImage.color = color;

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Clamp01(time/fadeDuration);
            globalFadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        globalFadeImage.color = color;
    }

    public IEnumerator FadeInRoutine(float fadeDuration)
    {
        float time = 0f;
        Color color = globalFadeImage.color;
        iconImage.gameObject.SetActive(false);
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (time / fadeDuration));
            globalFadeImage.color = color;
            yield return null;
        }
             
        globalFadeImage.raycastTarget = false;
        globalFadeCanvas.gameObject.SetActive(false);
    }
    
    // 추후 스테이지 넘어가는 UI 관리 코드 삽입 가능
}
