using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public enum IsaacCurrentState
{
    Idle, Move, Attack, Die
}

public class IsaacController : MonoBehaviour, ITakeDamageable
{
    #region variable
    [SerializeField] public GameObject head;
    [SerializeField] public GameObject body;
    [SerializeField] Camera camera;

    public StateMachine<IsaacController> stateMachine;
    private Rigidbody2D rb;
    private IsaacAnimController animController;
    //public Animator BodyAnimator { get; private set; }
    public IsaacInput Input { get; private set; }
    public Dictionary<IsaacCurrentState, IsaacState> iStateDic = new Dictionary<IsaacCurrentState, IsaacState>();
    private WaitForSeconds wait;
    private WaitForSeconds knockbakcWait;
    private IsaacInfo isaacInfo;
    private bool isKnockback;
    private float knockbackForce;
    private float knockbackTime;
    private readonly Color hitRed = new Color(5f, 0, 0, 1f);
    #endregion

    private void Awake()
    {
        iStateDic.Clear();
        animController = GetComponent<IsaacAnimController>();
        rb = GetComponent<Rigidbody2D>();
        Input = GetComponent<IsaacInput>();

        stateMachine = new StateMachine<IsaacController>(this);
        isKnockback = false;
        knockbackTime = 0.5f;
        knockbakcWait = new WaitForSeconds(knockbackTime);
        knockbackForce = 2f; // 임시방편
    }

    void Start()
    {
        iStateDic[IsaacCurrentState.Idle] = new IsaacIdleState(this, animController, rb, isaacInfo);
        iStateDic[IsaacCurrentState.Move] = new IsaacMoveState(this, animController, rb, isaacInfo);
        iStateDic[IsaacCurrentState.Attack] = new IsaacAttackState(this, animController, rb, isaacInfo);
        iStateDic[IsaacCurrentState.Die] = new IsaacDieState(this, animController, rb, isaacInfo);

        // 게임 시작할 때 Extra SpriteRenderer.Color.a = 0;
        // 게임 제일 처음 시작 시 2초간 움직일 수 없음
        // IsaacData를 받아 온 후에 StarAnimTime 실행해야함 => 안그러면 초기화 값 안들어감
        // 현재는 IsaacManager에서 하지만 추후 GameManager 또는 StageManager에서 할 예정
        isaacInfo = IsaacManager.Instance.GameStart( () => 
            {
                StartAnimTime(2f, () => { stateMachine.ChangeState(iStateDic[IsaacCurrentState.Idle]); });
            });
    }


    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
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

    public void TakeDamage(float damage, Vector2 damageDir) 
    {
        if (damage == 0 || isKnockback)
            return;

        IsaacManager.Instance.TakeDamage(damage, () =>
        {
            Dead();
            return;
        });
        Knockback(damageDir);
    }

    private void Dead()
    {
        stateMachine.ChangeState(iStateDic[IsaacCurrentState.Die]);
    }

    public void Knockback(Vector2 damageDir) 
    {
        Debug.Log("아이작 넉백 발생");
        isKnockback = true;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(damageDir.normalized * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(HitFlash());
    }
    public IEnumerator HitFlash() 
    {
        animController.SetAnimTrigger(IsaacAnimState.Hit, true);
        yield return knockbakcWait;
        animController.SetAnimTrigger(IsaacAnimState.Hit, false);
        isKnockback = false;
        rb.linearVelocity = Vector2.zero;
    }

    // 미완성 상태
    private void GoToNextStage()
    {
        // 스테이지 클리어 순서
        // 지하로 내려가는 오브젝트 충돌 시 -> FallDown 애님 시작
        // 종료 후 StageManager에 클리어 소식 전달 후 내려간 UI 출력
        // timesacle = 0
        // UI 종료 후 Appear 애님 시작
        // 종료 후 timesale = 1 / Idle 상태에서 시작
        StageManager.Instance.StageClear( 
            () => 
            { 
                animController.SetAnimTrigger(IsaacAnimState.FallDown, true);
                animController.SetAnimTrigger(IsaacAnimState.Appear, true);
            });
        animController.FallDownAnim();
    }
}
