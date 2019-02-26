using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnStateExit : StateMachineBehaviour {

    private Action<HexagonTille> moveAction;
    private HexagonTille moveDestination;

    public void SetMoveAction(Action<HexagonTille> moveAction, HexagonTille moveDestination)
    {
        this.moveAction = moveAction;
        this.moveDestination = moveDestination;
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveAction(moveDestination);
        //moveAction = null;
        //moveDestination = null;
    }
}
