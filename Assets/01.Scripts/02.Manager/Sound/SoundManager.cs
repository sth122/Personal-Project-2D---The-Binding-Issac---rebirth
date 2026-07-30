using System.Collections;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip introBGM;    
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip loadSceneBGM;

    [SerializeField] private AudioClip pageTurnSFX;

    [SerializeField] private AudioClip scrollSFX;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        PlayIntroTitleBGM();
    }

    public void PlayIntroTitleBGM()
    {
        if (introBGM != null && titleBGM != null)
        {
            StartCoroutine(IntroToTitleBGM());
        }
        else
        {
            Debug.LogError("PlayBGM clip null error");
            return;
        }
    }
    private IEnumerator IntroToTitleBGM()
    {
        bgmSource.clip = introBGM;
        bgmSource.loop = false;
        bgmSource.Play();

        yield return new WaitForSeconds(introBGM.length - 1.22f);

        bgmSource.clip = titleBGM;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    private void PlayBgm(AudioClip clip, bool isLoop)
    {
        bgmSource.clip = clip;
        bgmSource.loop = isLoop;
        bgmSource.Play();
    }
    private void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("PlaySFX clip null error");
            return;
        }
        sfxSource.PlayOneShot(clip);
    }

    public void PlayLoadSceneBGM()
    {
        PlayBgm(loadSceneBGM, false);
    }

    public void PlayPageTurn()
    {
        PlaySFX(pageTurnSFX);
    }

    public void PlayScrollSFX()
    {
        PlaySFX(scrollSFX);
    }

}

