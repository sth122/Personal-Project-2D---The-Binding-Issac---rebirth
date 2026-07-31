using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IsaacSceneManager : Singleton<IsaacSceneManager>
{
    private bool isTransitioning = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void LoadSceneWhiteFade(string sceneName, float duration = 1f)
    {
        if (isTransitioning) 
            return;
        StartCoroutine(TranstionRoutine(sceneName, Color.white, duration));
    }
    public void LoadSceneBlakcFade(string sceneName, float duration = 3f)
    {
        if (isTransitioning)
            return;

        SoundManager.Instance.PlayLoadGameSceneBGM();
        StartCoroutine(TranstionRoutine(sceneName, Color.black, duration));
    }

    private IEnumerator TranstionRoutine(string sceneName, Color fadeColor, float fadeDuration)
    {
        isTransitioning = true;


        // 신 넘어가는 Fade In 연출
        yield return StartCoroutine(UIManager.Instance.FadeOutRoutuine(fadeColor, fadeDuration));
        
        // 신 이동
        SceneManager.LoadScene(sceneName);

        yield return new WaitForSeconds(0.1f);

        // 신 넘어가는 Fade Out 연출
        yield return StartCoroutine(UIManager.Instance.FadeInRoutine(fadeDuration));

        isTransitioning = false;
    }
}
