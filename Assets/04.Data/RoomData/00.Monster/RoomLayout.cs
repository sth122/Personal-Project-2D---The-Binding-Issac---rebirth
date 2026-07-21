
using System.Collections.Generic;
using System;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[Serializable]
public class SpawnInfo
{
    public int ID;
    public Vector3 position;
}

[CreateAssetMenu(fileName = "RoomLayout", menuName = "Data/RoomLayOut")]
public class RoomLayoutData : ScriptableObject
{
    [SerializedDictionary("Room Type", "Room Layout")]
    public SerializedDictionary<RoomType, List<SpawnInfo>> RoomList = new SerializedDictionary<RoomType, List<SpawnInfo>>();

}
