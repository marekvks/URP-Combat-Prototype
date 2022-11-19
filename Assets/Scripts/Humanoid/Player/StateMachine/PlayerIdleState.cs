using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.PlayerAnimator.SetBool("Idle", true);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(xInput, 0f, yInput).normalized;

        if (direction.magnitude >= 0.1)
        {
            player.PlayerAnimator.SetBool("Idle", false);
            player.SwitchState(player.WalkingState);
        }
    }
}
