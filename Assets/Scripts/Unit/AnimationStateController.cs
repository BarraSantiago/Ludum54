using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [Header("Setup")]
    private Animator animator;

    string MoveKey = "Move";
    string MoveEndKey = "MoveEnd";
    string AttackKey = "Attack";
    string DieKey = "Die";

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public bool GetMoveAnimation()
    {
        return animator.GetBool(MoveKey);
    }

    public void SetMoveAnimation(bool move)
    {
        if (!animator.GetBool(MoveKey))
        {
            animator.SetBool(MoveKey, move);
        }
    }

    public void SetMoveEndAnimation(bool move)
    {
        if (!animator.GetBool(MoveEndKey))
        {
            animator.SetBool(MoveEndKey, move);
        }
    }

    public void SetDieAnimation(bool move)
    {
        if (!animator.GetBool(DieKey))
        {
            animator.SetBool(DieKey, move);
        }
    }

    public void SetAttackAnimation(bool move)
    {
        if (!animator.GetBool(AttackKey))
        {
            animator.SetBool(AttackKey, move);
        }
    }
}
