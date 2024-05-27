
using UnityEngine;
using UnityEngine.UIElements;

public class MoveState : IState
{
    private Player _player;
    private IStateSwitcher _switcher;

    private float moveSpeed = 4f;
    public MoveState(Player player, IStateSwitcher switcher)
    {
        _player = player;
        _switcher = switcher;
    }
    public void Enter()
    {

    }

    public void InputHandle()
    {
        _player.MoveDirection = new Vector3(0, _player.MoveDirection.y, Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
            _player.Jump();
    }

    public void Update()
    {
        _player.GetCharacterController().Move(_player.MoveDirection * moveSpeed * Time.deltaTime);
        _player.RotatePlayer(8f);

        _player.GetPlayerAnimator().UpdateMoveAnim("VelX", _player.MoveDirection.z);
        
        if (!_player.GetCharacterController().isGrounded)
            _switcher.SwitchState<AirState>();

    }
    public void Exit()
    {
        _player.GetPlayerAnimator().StopMoveAnim("VelX");
    }

}
