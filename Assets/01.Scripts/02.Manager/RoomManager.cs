using System;
using UnityEngine;

public enum RoomType
{
    Start, Normal, Boss, Treasure, Shop, Secret, Devil, Angel
}


public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] public GameObject Player;
    public RoomLayoutData roomLayoutData;
    [SerializeField] GameObject room;

    private RoomType currentRoomType;

    private Room currentRoom = new Room();
    private Room[,] roomArray = new Room[5, 5];    // 방 배열을 저장

    protected override void Awake()
    {
        base.Awake();
        roomLayoutData.Init();
    }

    
    private void Start()
    {
        // 초기 테스트  타입
        // 방 생성할 시 타입 정하게 해야함
        currentRoomType = RoomType.Normal;
        Init(() =>
        {
            SetFirstRoom();
            SetRoomType();
        });
    }

    /// <summary>
    /// 제일 처음 생성되는 정중앙 Room 
    /// </summary>
    private void SetFirstRoom()
    {
        GameObject centerRoom = ObjectPoolManager.Instance.GetObject("Room");
        if (centerRoom != null)
        {
            Debug.Log("Room 생성");
            currentRoom = centerRoom.GetComponent<Room>();
            currentRoom.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogError("centerRoom null");
        }
        CameraRoomRock.Instance.SetCameraPosition(centerRoom.transform);
    }

    public void Init(Action OnAction)
    {
        OnAction?.Invoke();
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
            int idx = UnityEngine.Random.Range(0, roomCount);
            // 참조 복사 해결해야함
            RoomEntityData roomData = roomLayoutData.RoomDic[currentRoomType][idx].Clone();
            roomData.SetLocalToWroldRoomPostionn(currentRoom.transform.position);
            SpawnManager.Instance.SpawnAll(roomData);
        }
    }
}
