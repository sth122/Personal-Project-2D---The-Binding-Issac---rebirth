using System;
using System.Buffers.Text;
using UnityEditor.SceneManagement;
using UnityEngine;

public interface ITraceable
{
    public void Trace();
}
public interface IAttackable
{
    public void Attack();
}

abstract public class MonsterController : MonoBehaviour
{
	#region variable
	public StateMachine<MonsterController> stateMachine;
	public MonsterIdleState mIdleState;
	public MonsterMoveState mMoveState;
    public MonsterTraceState mTraceState;
    [SerializeField] protected Transform target;

    private Rigidbody2D rb;
    public Rigidbody2D RB { get { return rb; } private set { rb = value; } }
    [SerializeField]protected Monster mStat;
    protected MonsterAnim animController;
    protected SpriteRenderer sr;
    #endregion

    protected virtual void Awake()
    {
        stateMachine = new StateMachine<MonsterController>(this);
        mIdleState = new MonsterIdleState(this);
        mMoveState = new MonsterMoveState(this);
        mTraceState = new MonsterTraceState(this);
        animController = new MonsterAnim();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        stateMachine.ChangeState(mIdleState);
    }

    protected virtual void Update()
    {
        stateMachine.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void InitData(Monster data, Transform target)
    {
        mStat = data.Clone();
        mStat.SetTotalHp();
        OnDataLodead();
        this.target = target;
    }

    protected virtual void OnDataLodead() { }

}
