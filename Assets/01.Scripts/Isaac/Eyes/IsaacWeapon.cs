using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player 무기 추상 클래스 스크립트 = Player's weapon abstract class script
/// </summary>
abstract public class IsaacWeapon : MonoBehaviour
{
    #region protected Variable
    [SerializeField] protected int damage;
    [SerializeField] GameObject isaac;

    protected float delay;
    protected bool canAttack;
    private Animator headAnimator;
    public IsaacInput Input { get; private set; }
    private WaitForSeconds wait;
    private SpriteRenderer sr;
    private Vector2 headDirection;
    private Vector2 attackDirection;
    private bool isAttack;
    #endregion

    #region head animation variable
    public int IsHeadMove { get; private set; }
    public int IsHeadUpDown { get; private set; }
    public int IsHeadLeftRightAttack { get; private set; }
    public int IsHeadUpDownAttack { get; private set; }
    #endregion

    private void Awake()
    {
        headAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Input = isaac.GetComponent<IsaacInput>();
    }

    protected virtual void Start()
    {
        AnimationInitialize();

        canAttack = true;
        delay = 1f;
        wait = new WaitForSeconds(delay);
        StartCoroutine(AttackDelay());
    }

    protected abstract void Attack();

    public void LookHead()
    {
        attackDirection = Input.IsaacActions.Attack.ReadValue<Vector2>();
        isAttack = attackDirection != Vector2.zero;

        if (isAttack)
        {
            //AnimationUpdate(attackDirection);
        }
        else
        {
            headDirection = Input.IsaacActions.Move.ReadValue<Vector2>();
            //AnimationUpdate(headDirection);
        }

    }

    IEnumerator AttackDelay()
    {
        while (true)
        {
            yield return new WaitWhile(() => canAttack);
            yield return wait;
            canAttack = true;
        }
    }

    private void AnimationInitialize()
    {
        IsHeadMove = Animator.StringToHash("isHeadMove");
        IsHeadUpDown = Animator.StringToHash("isHeadUpDown");
        IsHeadLeftRightAttack = Animator.StringToHash("isHeadLeftRightAttack");
        IsHeadUpDownAttack = Animator.StringToHash("isHeadUpDownAttack");
    }
    //private void AnimationUpdate(Vector2 dir)
    //{
    //    if (dir.y != 0)
    //    {
    //        headAnimator.SetBool(upDownHash, true);
    //        headAnimator.SetBool(moveHash, false);
    //        return;
    //    }
    //    else
    //    {
    //        headAnimator.SetBool(upDownHash, false);
    //    }

    //    if (dir.x != 0)
    //    {
    //        headAnimator.SetBool(moveHash, true);
    //        if (dir.x > 0)
    //        {
    //            sr.flipX = false;
    //        }
    //        else
    //        {
    //            sr.flipX = true;
    //        }
    //    }
    //    else
    //    {
    //        headAnimator.SetBool(moveHash, false);
    //    }
    //}
}