using System;
using UnityEngine;
using System.Collections.Generic;

public enum MoveType
{
    Walk, Fly, Jump, Fix
}

[Serializable]
public class MonsterInfo
{
    public int monsterID;
    public string name;
    public float totalHp;
    public float baseHp;
    public float damage;
    public float contactDamage;
    public float stageHp;
    public float speed;
    public float appearAnimTime;
    public float dieAnimTime;
    public EntityType type = EntityType.Monster;
    public MoveType moveType;

    //private float totalHp;
    public MonsterInfo(int monsterID, string name, float baseHp, float damage, float contactDamage, float stageHp, float speed, float appearAnimTime, float dieAnimTime,MoveType moveType)
    {
        this.monsterID = monsterID;
        this.name = name;
        this.baseHp = baseHp;
        this.damage = damage;
        this.contactDamage = contactDamage;
        this.stageHp = stageHp;
        this.speed = speed;
        this.moveType = moveType;
        this.appearAnimTime = appearAnimTime;
        this.dieAnimTime = dieAnimTime;
    }

    public MonsterInfo Clone() { return new MonsterInfo(monsterID, name, baseHp, damage, contactDamage, stageHp, speed, appearAnimTime,dieAnimTime,moveType); }
    
    public void SetTotalHp()
    {
        totalHp = baseHp + (stageHp * (Mathf.Min(4, StageManager.Instance.stageCnt) + 0.8f * Mathf.Max(0.0f, StageManager.Instance.stageCnt - 5.5f)));
    }
}

[CreateAssetMenu(fileName = "MonsterData", menuName = "Data/MonsterData")]
public class MonsterData : ScriptableObject
{
    public List<MonsterInfo> monsterList = new List<MonsterInfo>();
}