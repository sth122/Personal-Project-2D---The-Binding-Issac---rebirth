using UnityEngine;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    public int x, y;
    private bool isClear;
    public RoomEntityData roomEntity;

    Vector3 position;

    public void SetSpawnPostion(Vector3 currentRoomPos)
    {
        if (currentRoomPos == null)
            Debug.LogError("currentRoomPos null");

        position = new Vector3();
    }
}
