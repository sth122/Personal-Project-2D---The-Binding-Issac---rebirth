using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class EnddingManager : Singleton<EnddingManager>
{
    [SerializeField] private VideoPlayer videoPlayer;

    private string nextSceneName = "MainMenuScene";

    private bool isTranstioning = false;

    protected override void Awake()
    {
        isDDOL = false;
        base.Awake();
    }

    public void StartEnddingCredit()
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

        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
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
