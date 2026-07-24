using UnityEngine;
using System;
using System.Collections.Generic;
public enum TearType
{
    BasicTears, BloodTears, MonsterTears
}

public class TearsFactory
{
    private Dictionary<TearType, string> tears = new Dictionary<TearType, string>();

    public TearsFactory()
    {
        foreach (TearType type in Enum.GetValues(typeof(TearType)))
        {
            tears[type] = type.ToString();
        }
    }

    public GameObject OnSpawnBullet(TearType type)
    {
        if (tears.ContainsKey(type))
        {
            return ObjectPoolManager.Instance.GetObject(tears[type]);
        }
        else
        {
            Debug.LogError("Tears null error");
            return null;
        }
    }
}
