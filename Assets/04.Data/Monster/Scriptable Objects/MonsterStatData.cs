using System;
using UnityEngine;
using System.Collections.Generic;

public enum MoveType
{
    Walk, Fly, Jump, Fix
}

[Serializable]
public class Monster
{
    public int monsterID;
    public string name;
    public float totalHp;
    public float baseHp;
    public float damage;
    public float contactDamage;
    public float stageHp;
    public float speed;
    public float traceRange;
    public float attackRange;
    public MoveType moveType;

    //private float totalHp;
    public Monster(int monsterID, string name, float baseHp, float damage, float contactDamage, float stageHp, float speed, MoveType moveType)
    {
        this.monsterID = monsterID;
        this.name = name;
        this.baseHp = baseHp;
        this.damage = damage;
        this.contactDamage = contactDamage;
        this.stageHp = stageHp;
        this.speed = speed;
        this.moveType = moveType;
    }

    public Monster Clone() { return new Monster(monsterID, name, baseHp, damage, contactDamage, stageHp, speed, moveType); }

    public void SetTotalHp()
    {
        totalHp = baseHp + (stageHp * (Mathf.Min(4, StageManager.Instance.stageCnt) + 0.8f * Mathf.Max(0.0f, StageManager.Instance.stageCnt - 5.5f)));
    }
}

[CreateAssetMenu(fileName = "MonsterStatData", menuName = "Data/Monster")]
public class MonsterStatData : ScriptableObject
{
    public List<Monster> monsterList = new List<Monster>();
}