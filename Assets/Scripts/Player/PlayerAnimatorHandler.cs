using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHandler
{
    private Player _player;
    private Animator _animator;
    public PlayerAnimatorHandler(Player player, Animator animator)
    {
        _player = player;   
        _animator = animator;
    }

    public void UpdateMoveAnim(string name, float value) => _animator.SetFloat(name, Mathf.Abs(value * 2));
    public void StopMoveAnim(string name) => _animator.SetFloat(name, 0f);

    public void SetupStatusAirAnim(string name, bool status) => _animator.SetBool(name, status);
    public void UpdateAirAnim(string name, float value) => _animator.SetFloat(name, value);
    public void StopAirAnim(string name) => _animator.SetFloat(name, 0f);

    public void SetupStatusDeadAnim(string name,bool status) => _animator.SetBool(name,status); 

    public void SetupStatusWinAnim(string name, bool status) => _animator.SetBool(name, status);

}
