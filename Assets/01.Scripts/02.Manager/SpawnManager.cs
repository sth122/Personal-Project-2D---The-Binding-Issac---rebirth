using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] GameObject Player;
    public MonsterStatData monsterStatData;

    public RoomLayoutData roomLayoutData;

    private void Start()
    {
        foreach(var spawnInfo in roomLayoutData.spawnList)
        {
            Monster mStat = monsterStatData.monsterList.Find(x => x.monsterID == spawnInfo.monsterID);
            var monster = ObjectPoolManager.Instance.Get(mStat.Clone().name);

            if(monster != null)
            {
                MonsterController mController = monster.GetComponent<MonsterController>();
                mController.InitData(mStat, Player.transform);
            }
        }
    }
}
