using System.Collections;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioSource sfxSource;

    #region mainMenu
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip loadGameSceneBGM;
    [SerializeField] private AudioClip pageTurnSFX;
    [SerializeField] private AudioClip scrollSFX;
    #endregion

    #region inGame
    [SerializeField] private AudioClip stageBGM;
    [SerializeField] private AudioClip bossRoomBGM;
    [SerializeField] private AudioClip bossClearBGM;


    [SerializeField] private AudioClip enterTheBossRoomSFX;
    [SerializeField] private AudioClip[] isaacHitSFX;
    [SerializeField] private AudioClip[] isaacDieSFX;
    [SerializeField] private AudioClip tearFireSFX;
    [SerializeField] private AudioClip tearBlockSFX;


    [SerializeField] private AudioClip bossDieSFX;
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


    #region BGM
    public void PlayIntroTitleBGM()
    {
        if (titleBGM != null)
        {
            PlayBgm(titleBGM, true);
        }
        else
        {
            Debug.LogError("PlayBGM clip null error");
            return;
        }
    }

    private IEnumerator CoroutineBGM(AudioClip firstClip, AudioClip nextClip)
    {
        PlayBgm(firstClip, false);

        yield return new WaitForSeconds(firstClip.length);

        PlayBgm(nextClip, true);

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
    public void PlayLoadGameSceneBGM() => currentBGMCoroutine = StartCoroutine(CoroutineBGM(loadGameSceneBGM, stageBGM));

    public void PlayBossRoomBGM() => PlayBgm(bossRoomBGM, true);
    #endregion

    #region SFX
    private void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("PlaySFX clip null error");
            return;
        }
        sfxSource.PlayOneShot(clip);
    }

    public void PlayPageTurnSFX() => PlaySFX(pageTurnSFX);

    public void PlayScrollSFX() => PlaySFX(scrollSFX);
    public void PlayTearFireSFX() => PlaySFX(tearFireSFX);
    public void PlayTearBlockSFX() => PlaySFX(tearBlockSFX);
    public void PlayEnterTheBossRoomSFX()
    {
        bgmSource.Stop();
        PlaySFX(enterTheBossRoomSFX);
    }
    public void PlayIsaacHitSFX()
    {
        int cnt = isaacHitSFX.Length;
        int idx = Random.Range(0, cnt);
        PlaySFX(isaacHitSFX[idx]);
    }
    public void PlayIsaacDieSFX()
    {
        int cnt = isaacDieSFX.Length;
        int idx = Random.Range(0, cnt);

        PlaySFX(isaacDieSFX[idx]);
    }
    #endregion

    public void PlayStageClearBGM()
    {
        PlaySFX(bossDieSFX);
        PlayBgm(bossClearBGM, false);
    }
}

