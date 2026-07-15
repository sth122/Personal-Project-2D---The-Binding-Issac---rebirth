using UnityEngine;

// Appear : 소환 시 나타내는 Anim
// Idle : 멈춘 상태 Anim
// Move : 움직이는 상태 Anim

public class MonsterAnimController : MonoBehaviour
{
	#region varialbe
	private Animator mAnimator;
    protected int IsMove {  get; private set; }
    protected int IsDied { get; private set; }
    #endregion

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }

    private void AnimationInitialize()
    {

    }
}
