using System.Collections;
using UnityEngine;


/// <summary>
/// Player 무기 추상 클래스 스크립트 = Player's weapon abstract class script
/// </summary>
abstract public class IsaacWeapon : MonoBehaviour
{
    #region protected Variable
    [SerializeField] protected int damage;
    [SerializeField] GameObject isaac;

    protected float tearDelay;
    protected bool canAttack;
    public IsaacInput Input { get; private set; }
    private WaitForSeconds wait;
    protected Vector2 attackDirection;
    protected AttackInputDirection headDirection;
    protected int posDir;
    #endregion

    private void Awake()
    {
        Input = isaac.GetComponent<IsaacInput>();
    }

    protected virtual void Start()
    {
        canAttack = true;
        tearDelay = 0.5f;
        wait = new WaitForSeconds(tearDelay);
        posDir = 1;
        StartCoroutine(AttackDelay());
    }

    protected virtual void Update()
    {

    }

    protected abstract void Attack();

    IEnumerator AttackDelay()
    {
        while (true)
        {
            yield return new WaitWhile(() => canAttack);
            yield return wait;
            posDir *= -1;
            canAttack = true;
        }
    }
}