using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public int stageCnt;

    protected override void Initialize()
    {
        stageCnt = 1;
    }
    public void StageClear()
    {
        stageCnt++;
    }
}
