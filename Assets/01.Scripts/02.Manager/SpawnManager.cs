using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public MonsterStatData monsterStatData;

    public RoomLayoutData roomLayoutData;

    private void Start()
    {
        foreach(var spawnInfo in roomLayoutData.spawnList)
        {
            Monster mStat = monsterStatData.monsterList.Find(x => x.monsterID == spawnInfo.monsterID);

            var monster = MonsterPoolManager.Instance.Get(mStat.Clone().name);

            if(monster != null)
            {
                MonsterController mController = monster.Component;
                mController.InitData(mStat);
            }
        }
    }
}
