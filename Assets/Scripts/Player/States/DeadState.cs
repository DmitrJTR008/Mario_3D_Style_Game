using System;
using UnityEngine;
public class DeadState : IState
{
    private const string DeadAnimName = "isDead";
    private Player _player;
    private IStateSwitcher _switcher;
    public DeadState(Player player, IStateSwitcher switcher)
    {
        _player = player;
        _switcher = switcher;
    }
    public void Enter()
    {
       Debug.Log("Dead");
        _player.GetPlayerAnimator().SetupStatusDeadAnim(DeadAnimName, true);
    }

    public void Exit()
    {
    }

    public void InputHandle()
    {
    }

    public void Update()
    {
    }
}
