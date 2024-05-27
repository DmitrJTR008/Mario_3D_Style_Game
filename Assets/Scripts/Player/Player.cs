using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnGetHealth;
    public event Action OnPlayerDead;
    public event Action<int> OnGetDamage;
    public event Action<int> OnConsumeHealth;

    [SerializeField] private Transform headTop;
    [SerializeField] private Material playerMaterial;

    public Vector3 MoveDirection;

    private CharacterStateMachine _stateMachhine;
    private PlayerAnimatorHandler _animatorHandler;
    private CharacterController _controller;
    private PlayerBonusBox _bonusBox;

    private float jumpForce = 1.5f;
    private int _healtCount;
    private bool _isDead;

    public CharacterController GetCharacterController() => _controller;
    public Transform HeadTransform => headTop;
    public PlayerAnimatorHandler GetPlayerAnimator() => _animatorHandler;

    private void Update()
    {
        _stateMachhine?.HandleInput();
        _stateMachhine?.Update();
    }

    private bool IsGround() => _controller.isGrounded;

    public void RotatePlayer(float speed)
    {
        if (MoveDirection.z == 0) return;
        float targetAngle = MoveDirection.z < 0 ? -180 : 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetAngle, 0), speed * Time.deltaTime);
    }

    public void Jump()
    {
        if (IsGround())
            MoveDirection.y = jumpForce;
    }

    public void ForceJump(float force)
    {
        MoveDirection.y = force;
    }


    public void AcceptDamage()
    {
        if (_bonusBox.IsImmortal()) return;

        _healtCount--;
        OnGetDamage?.Invoke(_healtCount);

        if (_healtCount <= 0)
            KillPlayer();
        else
        {
            _bonusBox.EnableImmortal(playerMaterial, 3f);
            MoveDirection = Vector3.up;
        }
    }

    public void KillPlayer()
    {
        _stateMachhine.SwitchState<DeadState>();
        OnPlayerDead?.Invoke();
        _isDead = true;
    }

    public void ConsumeBonus(IBonus bonus)
    {
        bonus.Consume(this);
    }

    public void AddHealth()
    {
        if (_isDead) return;
        _healtCount++;
        _healtCount = Mathf.Clamp(_healtCount, 0, 3);
        OnConsumeHealth?.Invoke(_healtCount);
    }

    public void AcceptFinish()
    {
        _stateMachhine.SwitchState<WinState>(); 
    }
    public bool IsImmortal ()=>_bonusBox.IsImmortal();
    public void InitCharacter(int healhCount)
    {
        int minHealth = 1;
        int maxHealth = 3;
        _controller = GetComponent<CharacterController>();
        _animatorHandler = new PlayerAnimatorHandler(this, GetComponent<Animator>());
        _stateMachhine = new CharacterStateMachine(this,_stateMachhine);
        _bonusBox = new PlayerBonusBox(this);
        _healtCount = Mathf.Clamp(healhCount, minHealth, maxHealth);
    }
}






















// private void CalcGravity()
// {
//     
//     if (IsGround())
//     {
//         _animator.SetBool("isGround", IsGround());
//         moveDir.y = -0.5f;
//         forceDown = 0;
//     }
//     else
//     {
//
//         if (_controller.velocity.y > 0)
//         {
//             RaycastHit hit;
//             float sphereRadius = .1f;
//             Vector3 sphereOrigin = headTop.position;
//             Vector3 sphereDirection = Vector3.up;
//             float maxDistance = 0.1f;
//
//             if (Physics.SphereCast(sphereOrigin, sphereRadius, sphereDirection, out hit, maxDistance))
//             {
//                 Debug.Log("Hit detected: " + hit.collider.name);
//                 IBrick brick = hit.collider.GetComponent<IBrick>();
//                 if (brick != null)
//                 {
//                     Debug.Log("Brick detected: " + hit.collider.name);
//                     brick.Interact();
//                 }
//                 else
//                 {
//                     Debug.Log("IBrick component not found on: " + hit.collider.name);
//                 }
//
//                 moveDir.y = 0f;
//             }
//         }
//
//         forceDown += Time.deltaTime * 1.5f;
//         forceDown = Mathf.Clamp(forceDown, 0, 1);
//         moveDir.y -= (forceDown * gravityForce) * Time.deltaTime;
//     }
// }


// private void CalcRotation()
// {
//     if (moveDir.z == 0) return;
//     float targetAngle = moveDir.z < 0 ? -180 : 0;
//     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetAngle, 0), 8 * Time.deltaTime);
// }



// moveDir = new Vector3(0, moveDir.y, Input.GetAxis("Horizontal") );
// CalcGravity();
//
// if (Input.GetKeyDown(KeyCode.Space))
//     Jump();
//
// _animator.SetBool("isGround", IsGround());
//
// _controller.Move(moveDir * moveSpeed * Time.deltaTime);
//
// _animator.SetFloat("VelX", Mathf.Abs(moveDir.z * 2));
// _animator.SetFloat("VelY", moveDir.y);
//
// CalcRotation();