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

    public void SetMoveAnimation(bool move)
    {
        animator.SetBool(MoveKey, move);
    }

    public void SetMoveEndAnimation(bool move)
    {
        animator.SetBool(MoveEndKey, move);
    }

    public void SetDieAnimation(bool move)
    {
        animator.SetBool(MoveKey, move);
    }

    public void SetAttackAnimation(bool move)
    {
        animator.SetBool(MoveKey, move);
    }
}
