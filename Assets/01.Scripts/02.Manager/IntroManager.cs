using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Image whiteFlashImage;

    private string nextSceneName = "MainMenuScene";
    private float flashDuration = 1f;

    private bool isTranstioning = false;

    private void Start()
    {
        if(whiteFlashImage != null)
        {
            whiteFlashImage.color=new Color(1f, 1f, 1f, 0f);
            whiteFlashImage.gameObject.SetActive(true);
        }

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
            StartCoroutine(FlashAndLoadScene());
        }
    }
    private void EndVideoEvent(VideoPlayer videoPlayer)
    {
        if(!isTranstioning)
        {
            StartCoroutine(FlashAndLoadScene());
        }
    }

    private IEnumerator FlashAndLoadScene()
    {
        isTranstioning = true;
        videoPlayer.Pause();

        whiteFlashImage.gameObject.SetActive(true);
        Color flashColor = whiteFlashImage.color;
        flashColor.a = 0.001f;

        float time = 0f;
        while(time < flashDuration)
        {
            time += Time.deltaTime;
            flashColor.a = Mathf.Clamp01(time / flashDuration);
            whiteFlashImage.color = flashColor;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }

}
