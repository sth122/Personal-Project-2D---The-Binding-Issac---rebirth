using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomFactory
{
    private float roomWidth = 20f;
    private float roomHight = 18f;
    private int centerGrid = 5;

    private Dictionary<RoomType, string> rooms = new Dictionary<RoomType, string>();

    public RoomFactory()
    {
        foreach (RoomType type in Enum.GetValues(typeof(RoomType)))
        {
            rooms[type] = type.ToString();
        }
    }

    public GameObject OnSpawnRoom(Vector2Int grid, RoomType type)
    {
        if (rooms.ContainsKey(type))
        {
            GameObject roomObject = ObjectPoolManager.Instance.GetObject(rooms[type]);
            Vector3 worldPos = new Vector3((grid.x - centerGrid) * roomWidth, (grid.y - centerGrid) * roomHight, 0);
            roomObject.transform.position = worldPos;

            if (roomObject != null)
            {
                Room room = roomObject.GetComponent<Room>();
                room.SetRoom(type);

                return roomObject;
            }
            else
            {
                Debug.LogError($"room Error. not have room Object");
                return null;
            }
        }
        else
        {
            Debug.LogError($"rooms Error. not have key{type}");
            return null;
        }

    }
}

