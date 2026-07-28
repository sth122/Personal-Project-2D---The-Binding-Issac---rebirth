using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum DirectionsEnum
{
    Up, Down, Left, Right
}

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] public GameObject Player;
    private RoomGenerator roomGenerator;
    private Dictionary<Vector2Int, Room> spawnRoomMap;
    private List<Room> roomsList;
    public Room currentRoom;

    private Dictionary<DirectionsEnum, Vector2Int> dirs = new Dictionary<DirectionsEnum, Vector2Int>()
    {
        {DirectionsEnum.Up, Vector2Int.up},
        {DirectionsEnum.Down, Vector2Int.down},
        {DirectionsEnum.Left, Vector2Int.left},
        {DirectionsEnum.Right, Vector2Int.right},
    };


    private Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    protected override void Awake()
    {
        base.Awake();
        roomGenerator = new RoomGenerator();
        roomsList = new List<Room>();
        spawnRoomMap = new Dictionary<Vector2Int, Room>();
    }

    public void StartRoomsSpawn()
    {
        spawnRoomMap.Clear();
        StartCoroutine(SpawnAllRooms());
    }

    private IEnumerator SpawnAllRooms()
    {
        yield return StartCoroutine(roomGenerator.GenerateMapGrid());
        // 초기 테스트  타입
        // 방 생성할 시 타입 정하게 해야함

        Debug.Log("방 소환 시작");
        foreach (var room in roomGenerator.roomMap)
        {
            GameObject roomObj = SpawnManager.Instance.SpawnRoom(room.Key.coordinate, room.Value);
            if (roomObj != null && roomObj.TryGetComponent<Room>(out var roomComp))
            {
                roomsList.Add(roomComp);
                spawnRoomMap[room.Key.coordinate] = roomComp;
            }
        }

        DoorInstallInTheRoom();
    }

    public void ChangeRoom(Room room)
    {
        if (currentRoom != null)
        {
            currentRoom.OnPlayerExitRoom();
        }
        currentRoom = room;
        CameraRoomRock.Instance.SetCameraPosition(room.transform);
    }

    private void DoorInstallInTheRoom()
    {
        foreach (var vec2 in spawnRoomMap)
        {
            foreach (var dir in dirs)
            {
                Vector2Int checkPos = vec2.Key + dir.Value;
                if (spawnRoomMap.ContainsKey(checkPos))
                {
                    vec2.Value.doorCoordinate[dir.Key] = spawnRoomMap[checkPos].roomType;
                }
            }
            vec2.Value.DoorInstall();
        }
    }

}
