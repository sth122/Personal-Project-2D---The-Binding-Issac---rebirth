using System.Buffers.Text;
using UnityEditor.SceneManagement;
using UnityEngine;

public interface ITraceable{}

abstract public class MonsterController : MonoBehaviour
{
	#region variable
	public StateMachine<MonsterController> stateMachine;
	public MonsterIdleState mIdleState;
	public MonsterMoveState mMoveState;
    public MonsterTraceState mTraceState;
    protected Transform target;

    private Rigidbody2D rb;
    public Rigidbody2D RB { get { return rb; } }
    protected Monster mStat;
    protected MonsterAnimController animController;
    #endregion

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        stateMachine.Update(this);
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedUpdate(this);
    }

    public void InitData(Monster data)
    {
        mStat = data.Clone();
        mStat.SetTotalHp();
        OnDataLodead();
    }

    protected virtual void OnDataLodead() { }

}
