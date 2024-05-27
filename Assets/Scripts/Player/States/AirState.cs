using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : IState
{
    private const string AirAnimVelocityName = "VelY";
    private const string AirAnimBoolName = "isGround";

    private Player _player;
    private IStateSwitcher _switcher;

    private float forceDown = 0;
    private float gravityForce = 9;

    private Vector3 airMoveDirection;
    public AirState(Player player, IStateSwitcher stateSwitcher)
    {
        _player = player;
        _switcher = stateSwitcher;
    }

    public void Enter()
    {
        _player.GetPlayerAnimator().SetupStatusAirAnim(AirAnimBoolName, false);
    }
    public void InputHandle()
    {
        airMoveDirection = Vector3.forward * Input.GetAxis("Horizontal");
    }

    public void Update()
    {
        if (_player.GetCharacterController().isGrounded)
        {
            _player.MoveDirection.y = -0.5f;
            forceDown = 0;
            _switcher.SwitchState<MoveState>();
        }
        else
        {
            

            if (_player.GetCharacterController().velocity.y > 0)
            {
                RaycastHit hit;
                float sphereRadius = .1f;
                Vector3 sphereOrigin = _player.HeadTransform.position;
                Vector3 sphereDirection = Vector3.up;
                float maxDistance = 0.1f;

                if (Physics.SphereCast(sphereOrigin, sphereRadius, sphereDirection, out hit, maxDistance))
                {
                    IBrick brick = hit.collider.GetComponent<IBrick>();
                   
                    if (brick != null)
                        brick.Interact();

                    _player.MoveDirection.y = 0f;
                }

            }

            forceDown += Time.deltaTime * 1.5f;
            forceDown = Mathf.Clamp(forceDown, 0, 1);

            _player.MoveDirection.y -= (forceDown * gravityForce) * Time.deltaTime;
            _player.MoveDirection.z = airMoveDirection.z;

            _player.GetCharacterController().Move(_player.MoveDirection * 4 * Time.deltaTime);
            _player.RotatePlayer(1.5f);
            _player.GetPlayerAnimator().UpdateAirAnim(AirAnimVelocityName, _player.MoveDirection.y);
        }
    }

    public void Exit()
    {
        _player.GetPlayerAnimator().StopAirAnim(AirAnimVelocityName);

        _player.GetPlayerAnimator().SetupStatusAirAnim(AirAnimBoolName, true);
    }
}
