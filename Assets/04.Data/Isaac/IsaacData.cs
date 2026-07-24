using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class IsaacInfo
{
    public int isaacID;
    public string isaacName;
    public float hp;
    public float damage;
    public float tearsDelay; // 공격 딜레이
    public float range; // 사거리
    public float shootSpeed;    // 투사체 속도
    public float speed; // 이동 속도
    public float luck;
    public float deamonProbability;
    public float angelProbability;

    public IsaacInfo(int isaacID, string isaacName, float hp, float damage, float tearsDelay, float range, float shootSpeed, float speed, float luck, float deamonProbability, float angelProbability)
    {
        this.isaacID = isaacID;
        this.isaacName = isaacName;
        this.hp = hp;
        this.damage = damage;
        this.tearsDelay = tearsDelay;
        this.range = range;
        this.shootSpeed = shootSpeed;
        this.speed = speed;
        this.luck = luck;
        this.deamonProbability = deamonProbability;
        this.angelProbability = angelProbability;
    }

    public IsaacInfo Clone()
    { return new IsaacInfo(isaacID, isaacName, hp, damage, tearsDelay, range, shootSpeed, speed, luck, deamonProbability, angelProbability); }
}

[CreateAssetMenu(fileName = "IsaacStatData", menuName = "Data/Isaac")]
public class IsaacData : ScriptableObject
{
    public List<IsaacInfo> isaacList = new List<IsaacInfo>();
}
