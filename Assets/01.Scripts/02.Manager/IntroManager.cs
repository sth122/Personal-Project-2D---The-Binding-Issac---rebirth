using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private string nextSceneName = "MainMenuScene";

    private bool isTranstioning = false;

    private void Start()
    {
        if (videoPlayer != null) 
        {
            videoPlayer.loopPointReached += EndVideoEvent;
        }
    }
    private void Update()
    {
        if (isTranstioning)
            return;

        if(Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SkipIntro();
        }
    }
    private void EndVideoEvent(VideoPlayer videoPlayer)
    {
        if (!isTranstioning)
        {
            IsaacSceneManager.Instance.LoadSceneWhiteFade(nextSceneName);
        }
    }
    private void SkipIntro()
    {
        isTranstioning = true;
        IsaacSceneManager.Instance.LoadSceneWhiteFade(nextSceneName);
    }
}
