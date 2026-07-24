using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo
{
    public int id;
    public string name;

}

[CreateAssetMenu(fileName = "ObjectData", menuName = "Data/ObjectData")]
public class ObjectData : ScriptableObject
{
    public List<ObjectInfo> objectList =  new List<ObjectInfo>();
}
