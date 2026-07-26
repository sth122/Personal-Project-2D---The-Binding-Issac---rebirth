using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] public GameObject Player;
    private RoomGenerator roomGenerator;
    
    protected override void Awake()
    {
        base.Awake();
    }
    
    private void Start()
    {
        // 1스테이지 맵 생성
        roomGenerator = new RoomGenerator();
        roomGenerator.GenerateMapGrid();
        // 초기 테스트  타입
        // 방 생성할 시 타입 정하게 해야함
        SpawnAllRooms();
    }

    private void SpawnAllRooms()
    {
        foreach(var room in roomGenerator.roomMap)
        {
            SpawnManager.Instance.SpawnRoom(room.Key, room.Value);
        }
    }
}
