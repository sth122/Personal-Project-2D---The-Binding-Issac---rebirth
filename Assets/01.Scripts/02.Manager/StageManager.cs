using System;
using System.Collections;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public int stageCnt;

    private void Start()
    {
        stageCnt = 1;
        RoomManager.Instance.StartRoomsSpawn();
    }
    public void BossClear()
    {
        SoundManager.Instance.PlayStageClearBGM();
        RoomManager.Instance.SetEscapeRoom();
    }

    public void StageClear(Action onAction)
    {

        // FallDown 애니메이션 실행
        // Appear 애니메이션 실행하는 타이밍에 timescale = 0

        onAction?.Invoke();

        // ShowFallDonwCredit 종료 후 timesacle = 1
        // Appear 애니메이션 실행

        // tirgger 둘 다 false

        stageCnt++;

        StartCoroutine(Ending());
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(2f);

        IsaacSceneManager.Instance.LoadSceneWhiteFade("EnddingScene");
    }

    public void ShowFallDownCredit()
    {
        // 떨어질 때 스테이지 넘어가는 영상 출력

        // anyKey 입력 시 skip

        //UIManager.Instance.
    }
}
