using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ID / 체력 / 데미지 / 눈물 딜레이 / 사거리 / 눈물(투사체) 속도 / 이속 / 행운
/// 기본 픽업 소지 아이템이 각각 다르나 편의를 위해 전부 0 0 0 으로 고정
/// </summary>
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

    public IsaacInfo(int isaacID, string isaacName, float hp, float damage, float tearsDelay, float range, float shootSpeed, float speed, float luck)
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
    }

    public IsaacInfo Clone()
    { return new IsaacInfo(isaacID, isaacName, hp, damage, tearsDelay, range, shootSpeed, speed, luck); }
}

[CreateAssetMenu(fileName = "IsaacStatData", menuName = "Data/Isaac")]
public class IsaacData : ScriptableObject
{
    public List<IsaacInfo> isaacList = new List<IsaacInfo>();
}
