using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct DoorInfo
{
    public DoorType doorType;

    public DoorInfo(DoorType doorType)
    {
        this.doorType = doorType;
    }
}

[Serializable]
public struct DoorPos
{
    public DirectionsEnum direction;
    public Quaternion rotation;
    public Vector3 postion;
}

[Serializable]
public struct TeleportPos
{
    public DirectionsEnum direction;
    public Vector3 position;
}
[CreateAssetMenu(fileName = "DoorData", menuName = "Data/DoorData")]
public class DoorData : ScriptableObject
{
    public List<DoorInfo> doors = new List<DoorInfo>();

    public Dictionary<DirectionsEnum, DoorPos> doorPosDic = new Dictionary<DirectionsEnum,DoorPos>();
    public List<DoorPos> doorPos =  new List<DoorPos>();

    public Dictionary<DirectionsEnum, TeleportPos> teleportPosDic = new Dictionary<DirectionsEnum, TeleportPos>();
    public List<TeleportPos> teleportPos = new List<TeleportPos>();

    public void Init()
    {
        foreach(DoorPos pos in doorPos)
        {
            if(!doorPosDic.ContainsKey(pos.direction))
            {
                doorPosDic[pos.direction] = pos;
            }
        }

        foreach(TeleportPos pos in teleportPos)
        {
            if(!teleportPosDic.ContainsKey(pos.direction))
            {
                teleportPosDic[pos.direction] =  pos;
            }
        }
    }
}
