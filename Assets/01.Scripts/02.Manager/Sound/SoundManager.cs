using System.Collections;
using System.Linq;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioSource sfxSource;

    #region mainMenu
    [SerializeField] private AudioClip introBGM;
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip loadGameSceneBGM;
    [SerializeField] private AudioClip pageTurnSFX;
    [SerializeField] private AudioClip scrollSFX;
    #endregion

    #region inGame
    [SerializeField] private AudioClip[] isaacHit;
    [SerializeField] private AudioClip[] isaacDie;

    private Coroutine currentBGMCoroutine;
    #endregion
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
            currentBGMCoroutine = StartCoroutine(IntroToTitleBGM());
        }
        else
        {
            Debug.LogError("PlayBGM clip null error");
            return;
        }
    }
    private IEnumerator IntroToTitleBGM()
    {
        PlayBgm(introBGM, false);

        yield return new WaitForSeconds(introBGM.length - 1.22f);

        PlayBgm(titleBGM, true);

        currentBGMCoroutine = null;
    }
    private void StopCurrentBGMCoroutine()
    {
        if (currentBGMCoroutine != null)
        {
            StopCoroutine(currentBGMCoroutine);
            currentBGMCoroutine = null;
        }
    }

    private void PlayBgm(AudioClip clip, bool isLoop)
    {
        StopCurrentBGMCoroutine();

        if (clip == null)
        {
            Debug.LogError("PlayBgm clip null error");
            return;
        }
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

    public void PlayLoadGameSceneBGM() => PlayBgm(loadGameSceneBGM, false);

    public void PlayPageTurn() => PlaySFX(pageTurnSFX);

    public void PlayScrollSFX() => PlaySFX(scrollSFX);

    public void PlayIsaacHit()
    {
        int cnt = isaacHit.Length;
        int idx = Random.Range(0, cnt);
        PlaySFX(isaacHit[idx]);
    }
    public void PlayIsaacDie()
    {
        int cnt = isaacDie.Length;
        int idx = Random.Range(0, cnt);

        PlaySFX(isaacDie[idx]);
    }


}

