using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo
{
    public int id;
    public string name;

    public ObstacleInfo(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public ObstacleInfo Clone()
    {
        return new ObstacleInfo(id, name);
    }
}

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Data/ObstacleData")]
public class ObstacleData
    : ScriptableObject
{
    public List<ObstacleInfo> obstacleList =  new List<ObstacleInfo>();
}
