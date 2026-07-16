using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Isaac
{
    public int isaacID;
    public string isaacName;
    public float damage;
    public float tearsDelay; // 공격 딜레이
    public float range; // 사거리
    public float shootSpeed;    // 투사체 속도
    public float speed; // 이동 속도
    public float luck;
    public float deamonProbability;
    public float angelProbability;

    public Isaac(int isaacID, string isaacName, float damage, float tearsDelay, float range, float shootSpeed, float speed, float luck, float deamonProbability, float angelProbability)
    {
        this.isaacID = isaacID;
        this.isaacName = isaacName;
        this.damage = damage;
        this.tearsDelay = tearsDelay;
        this.range = range;
        this.shootSpeed = shootSpeed;
        this.speed = speed;
        this.luck = luck;
        this.deamonProbability = deamonProbability;
        this.angelProbability = angelProbability;
    }

    public Isaac Clone()
    { return new Isaac(isaacID, isaacName, damage, tearsDelay, range, shootSpeed, speed, luck, deamonProbability, angelProbability); }
}

[CreateAssetMenu(fileName = "IsaacStatData", menuName = "Data/Isaac")]
public class IsaacStatData : ScriptableObject
{
    public List<Isaac> isaacList = new List<Isaac>();
}
