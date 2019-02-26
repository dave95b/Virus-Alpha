using System;
using UnityEngine;

public class VirusAnimationController : MonoBehaviour {

    private Animator animator;
    private int moveId;

    private Action<HexagonTille> moveAction;


    private void Start()
    {
        animator = GetComponent<Animator>();
        InitParameters();
    }

    public void Move(HexagonTille moveDestination)
    {
        animator.GetBehaviour<MoveOnStateExit>().SetMoveAction(moveAction, moveDestination);
        animator.SetTrigger(moveId);
    }

    private void InitParameters()
    {
        moveId = Animator.StringToHash("move");
    }

    public void SetMoveAction(Action<HexagonTille> action)
    {
        moveAction = action;
    }
}
