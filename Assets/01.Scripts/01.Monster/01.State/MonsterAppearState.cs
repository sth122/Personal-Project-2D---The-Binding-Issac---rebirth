using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAppearState : MonsterState
{
    public MonsterAppearState(MonsterController controller, MonsterData mData) : base(controller, mData)
    {
        this.controller = controller;
        this.mData = mData;
    }


    public override void Enter()
    {
        Debug.Log("MonsterAppearState 입장");
        controller.Appear();
    }

    public  override void Exit()
    {
        Debug.Log("MonsterAppearState 퇴장");
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    { }
}
