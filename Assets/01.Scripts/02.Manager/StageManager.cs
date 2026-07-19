using System;
using System.Collections;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public int stageCnt;

    protected override void Initialize()
    {
        stageCnt = 1;
    }
    public void StageClear(Action OnGoToNextStage)
    {
        // FallDown 애니메이션 실행
        // Appear 애니메이션 실행하는 타이밍에 timescale = 0

        OnGoToNextStage?.Invoke();

        ShowFallDownCredit();

        // ShowFallDonwCredit 종료 후 timesacle = 1
        // Appear 애니메이션 실행

        // tirgger 둘 다 false
        
        stageCnt++;
    }

    public void ShowFallDownCredit()
    {
        // 떨어질 때 스테이지 넘어가는 영상 출력

        // anyKey 입력 시 skip
    }


}
