using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RoomType
{
    Normal, Boss, Treasure, Shop, Secret, Devil, Angel
}



public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] GameObject Player;
    public RoomLayoutData roomLayoutData;
    [SerializeField] GameObject room;

    private RoomType currentRoomType;

    private Room currentRoom = new Room();
    private Room[,] roomArray = new Room[5, 5];    // 방 배열을 저장

    private void Awake()
    {
        roomLayoutData.Init();
    }

    private void Start()
    {
        var centerRoom = ObjectPoolManager.Instance.GetObject("Room");
        if (currentRoom != null)
        {
            Debug.Log("Room 생성");
            currentRoom = centerRoom.GetComponent<Room>();
            currentRoom.transform.position = Vector3.zero;
        }
        CameraRoomRock.Instance.SetCameraPosition(centerRoom.transform);
    }

    public void GetRoomType(RoomType type)
    {
        this.currentRoomType = type;
    }

    // Room Script에서 소환하거나 StageManager에서 
    // 나중에 entityInfo 추상클래스로 만들어서 통합


    public void SetRoomType()
    {
        // 만약 노멀 방을 설정하려고 할 때
        // 1.Normal 방의 개수 확인
        // 2.Normal 방


        int roomCount = roomLayoutData.RoomDic[currentRoomType].Count;

        if (roomCount > 0)
        {
            int idx = Random.Range(0, roomCount);
            var a= roomLayoutData.RoomDic[currentRoomType][idx];
            EntitySpawnManager.Instance.Spawn(a);
        }
    }


    public void OnSaveEntityInRoom()
    {
        //roomLayoutData.roomDatas

        //var room = roomLayoutData.RoomDic[currentRoomType][randomRoomIndex];

        //foreach(var entity in room.Values)
        //{

        //}


        //foreach (var entity in room.Values)
        //{
        //    if (entity == null)
        //    {
        //        Debug.LogError("RoomDic error");

        //    }
        //    MonsterInfo mStat = monsterData.monsterList.Find(x => x.monsterID == entity);
        //    var monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
        //    monster.transform.position = spawnInfo.position;

        //    if (monster != null)
        //    {
        //        MonsterController mController = monster.GetComponent<MonsterController>();
        //        mController.InitData(mStat, Player.transform);
        //        mController.Appear();
        //    }
        //}
    }

    private void OnSpawnMonster()
    {
        //foreach (var spawnInfo in roomLayoutData.RoomList[currentRoomType])
        //{
        //    MonsterInfo mStat = monsterStatData.monsterList.Find(x => x.monsterID == spawnInfo.ID);
        //    var monster = ObjectPoolManager.Instance.GetObject(mStat.Clone().name);
        //    monster.transform.position = spawnInfo.position;

        //    if (monster != null)
        //    {
        //        MonsterController mController = monster.GetComponent<MonsterController>();
        //        mController.InitData(mStat, Player.transform);
        //        mController.Appear();
        //    }
        //}
    }

}
