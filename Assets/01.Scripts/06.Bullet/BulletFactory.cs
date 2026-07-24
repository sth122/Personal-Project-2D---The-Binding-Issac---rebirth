using System.Collections.Generic;
using System;
using UnityEngine;

public enum TearType
{
    BasicTears, BloodZTears, MonsterTears
}

public class BulletFactory
{
    private Dictionary<TearType, string> bulletsDict = new();

    public BulletFactory()
    {
        foreach (TearType type in Enum.GetValues(typeof(TearType)))
        {
            bulletsDict[type] = type.ToString();
        }
    }

    public GameObject OnSpawnBullet(TearType type)
    {
        if(bulletsDict.TryGetValue(type, out string name))
        {
            return ObjectPoolManager.Instance.GetObject(name);
        }
        else
        {
            Debug.LogError("Tear return null");
            return null;
        }
    }
}