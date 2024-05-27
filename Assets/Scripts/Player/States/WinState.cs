using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : IState
{
    private const string DeadAnimName = "isWin";
    private Player _player;
    private IStateSwitcher _switcher;
    public WinState(Player player, IStateSwitcher switcher)
    {
        _player = player;
        _switcher = switcher;
    }
    public void Enter()
    {
        _player.GetPlayerAnimator().SetupStatusWinAnim(DeadAnimName, true);
    }

    public void Exit() { }

    public void InputHandle() { }
    public void Update() { }
}
