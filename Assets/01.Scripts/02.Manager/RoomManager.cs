using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] GameObject Player;
    public MonsterData monsterStatData;
    //public EntityData entityData;

    public RoomLayoutData roomLayoutData;

    private void Start()
    {
        foreach(var spawnInfo in roomLayoutData.spawnList)
        {
            MonsterInfo mStat = monsterStatData.monsterList.Find(x => x.monsterID == spawnInfo.monsterID);
            var monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
            monster.transform.position = spawnInfo.position;

            if(monster != null)
            {
                MonsterController mController = monster.GetComponent<MonsterController>();
                mController.InitData(mStat, Player.transform);
                mController.Appear();
            }
        }
    }
}
