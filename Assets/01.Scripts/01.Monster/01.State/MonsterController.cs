using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITraceable
{
    public void Trace();
}
public interface IAttackable
{
    public void Attack();
}
public interface ITakeDamageable
{
    public void TakeDamage(float damage, Vector2 dir);
    public void Knockback(Vector2 damageDir);
    public IEnumerator HitFlash();
}

public interface IReturnPool
{
    public void ReturnPool();
}

// 나중에 IsaacCurrentState랑 통합 예정
public enum MonsterCurrentState
{
    Idle, Move, Trace, Attack, Die
}

abstract public class MonsterController : Monster
{
	#region variable
	public StateMachine<MonsterController> stateMachine;
    //[SerializeField] protected Transform target;

    //protected Rigidbody2D rb;
    //public Rigidbody2D RB { get { return rb; } private set { rb = value; } }
    //[SerializeField] protected MonsterInfo mData;
    //protected MonsterAnimController animController;
    //public MonsterAnimController AnimController { get { return animController; } }
    //protected SpriteRenderer sr;
    //private WaitForSeconds wait;

    public Dictionary<MonsterCurrentState, MonsterState> mStateDic = new Dictionary<MonsterCurrentState, MonsterState>();
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<MonsterController>(this);

        mStateDic[MonsterCurrentState.Idle] = new MonsterIdleState(this, mData);
        mStateDic[MonsterCurrentState.Move] = new MonsterMoveState(this, mData);

        //animController = GetComponent<MonsterAnimController>();
        //rb = GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();
    }

    protected override void OnEnable()
    {
        if(mData != null)
        {
            Appear();
        }
    }


    protected override void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;

    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    //public void InitData(MonsterInfo data, Transform target)
    //{
    //    mData = data.Clone();
    //    mData.SetTotalHp();
    //    OnDataLodead();
    //    this.target = target;
    //    Appear();
    //}

    protected override void Appear()
    {
        StartAnimTime(mData.appearAnimTime, () => { stateMachine.ChangeState(mStateDic[MonsterCurrentState.Idle]); });
    }
    public override void Dead()
    {
        mData.speed = 0;
        AnimController.AnimationStart(MonsterCurrentState.Die);
        StartAnimTime(mData.dieAnimTime, () => ReturnPool());
        // ReturnPool에서 사망 이펙트 추가
    }

    //public void ReturnPool()
    //{
    //    RoomManager.Instance.currentRoom.OnObjectReturn(this.gameObject);
    //    ObjectPoolManager.Instance.ReturnObject(mData.name, this.gameObject);
    //}
    //public void StartAnimTime(float time, Action OnComplete)
    //{
    //    Debug.Log("StartAnimTime에 진입");
    //    StartCoroutine(AnimTime(time, OnComplete));
    //}
    //IEnumerator AnimTime(float time, Action OnComplete)
    //{
    //    wait = new WaitForSeconds(time);
    //    yield return wait;
    //    OnComplete?.Invoke();
    //}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Isaac") &&
        //    collision.gameObject.TryGetComponent<ITakeDamageable>(out ITakeDamageable isaac))
        //{
        //    isaac.TakeDamage(mData.contactDamage, rb.linearVelocity);
        //}
    }
}
