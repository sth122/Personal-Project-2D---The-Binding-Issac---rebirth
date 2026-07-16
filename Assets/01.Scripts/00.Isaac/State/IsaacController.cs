using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public enum IsaacCurrentState
{
    Idle, Move, Attack,
}

public class IsaacController : MonoBehaviour
{
    #region variable
    [SerializeField] public GameObject head;
    [SerializeField] public GameObject body;

    public StateMachine<IsaacController> stateMachine;
    private Rigidbody2D rb;
    private IsaacAnimController animController;
    //public Animator BodyAnimator { get; private set; }
    public IsaacInput Input { get; private set; }
    public Dictionary<IsaacCurrentState, IsaacState> iStateDic = new Dictionary<IsaacCurrentState, IsaacState>();
    private WaitForSeconds wait;
    private IsaacInfo isaacInfo;
    #endregion

    private void Awake()
    {
        iStateDic.Clear();
        animController = GetComponent<IsaacAnimController>();
        stateMachine = new StateMachine<IsaacController>(this);
        iStateDic[IsaacCurrentState.Idle] = new IsaacIdleState(this, animController, rb, isaacInfo);
        iStateDic[IsaacCurrentState.Move] = new IsaacMoveState(this, animController, rb, isaacInfo);
        iStateDic[IsaacCurrentState.Attack] = new IsaacAttackState(this, animController, rb, isaacInfo);
        Input = GetComponent<IsaacInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 게임 제일 처음 시작 시 2초간 움직일 수 없음
        // IsaacData를 받아 온 후에 StarAnimTime 실행해야함 => 안그러면 초기화 값 안들어감
        // 현재는 IsaacManager에서 하지만 추후 GameManager 또는 StageManager에서 할 예정
        //IsaacManager.Instance.GameStart(() => 
        //{
        //    StartAnimTime(2f, ()  => { stateMachine.ChangeState(iStateDic[IsaacCurrentState.Idle]); });
        //});
        StartAnimTime(2f, () => { stateMachine.ChangeState(iStateDic[IsaacCurrentState.Idle]); });
    }


    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster")
            )
        {
            if (collision is IAttackable iAttackalbe)
            {
                //IsaacManager.Instance.TakeDamage(iAttackalbe.ContactAttack(), );

            }
        }
    }

    public void StartAnimTime(float time, Action OnComplete)
    {
        StartCoroutine(AnimTime(time, OnComplete));
    }

    IEnumerator AnimTime(float time, Action OnComplete)
    {
        wait = new WaitForSeconds(time);
        yield return wait;
        OnComplete?.Invoke();
    }
}
