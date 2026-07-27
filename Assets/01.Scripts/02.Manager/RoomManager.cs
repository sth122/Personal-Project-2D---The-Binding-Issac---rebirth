using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] public GameObject Player;
    private RoomGenerator roomGenerator;
    private List<GameObject> roomList = new List<GameObject>();
    public Room currentRoom;
    
    protected override void Awake()
    {
        base.Awake();
        roomGenerator = new RoomGenerator();
    }

    public void StartRoomsSpawn()
    {
        roomList.Clear();
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
            roomList.Add(SpawnManager.Instance.SpawnRoom(room.Key.coordinate, room.Value));
        }
    }



    public void ChangeRoom(Room room)
    {
        if(currentRoom != null)
        {
            currentRoom.OnPlayerExitRoom();
        }
        currentRoom = room;
        CameraRoomRock.Instance.SetCameraPosition(room.transform);
        Debug.Log($"현재 Grid 위치 : [{currentRoom.transform.position.x} , {currentRoom.transform.position.y}]");
    }
    
}
