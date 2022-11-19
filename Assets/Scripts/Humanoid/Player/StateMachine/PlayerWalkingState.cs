using UnityEngine;

public class PlayerWalkingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("Walk", true);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //player.PlayerController.Walk();
    }
}
