using System;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    PickUp, Passive, Active, Accessories
}

public enum PickUpType
{
    Coin, Key, Bomb,
}

[Serializable]
public class ItemInfo
{
    public int id;
    public string name;
    public ItemType type;

    public ItemInfo(int id, string name, ItemType type)
    {
        this.id = id;
        this.name = name;
        this.type = type;
    }

    public ItemInfo Clone() { return new ItemInfo(id, name, type); }
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public List<ItemInfo> itemListDic = new List<ItemInfo>();
}
