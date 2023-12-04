using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [Header("Attack details")]
    public float[] attackMovement;
    public GameObject attackBox;

    public bool isBusy {  get; private set; }
    [Header("Move info")]
    [SerializeField] internal float moveSpeed = 8f;
    [SerializeField] internal float jumpForce = 12f;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    [SerializeField] internal float dashSpeed;
    [SerializeField] internal float dashDuration;
    public float dashDir { get; private set; }





    #region Input
    public float moveInputX { get; private set; }
    public bool jumpInput { get; private set; }
    public bool dashInput { get; private set; }
    public bool attackInput { get; private set; }
    #endregion



    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerDashState dashState { get; private set; }


    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine         = new PlayerStateMachine();

        idleState            = new PlayerIdleState(this, stateMachine, "Idle");
        moveState            = new PlayerMoveState(this, stateMachine, "Move");
        jumpState            = new PlayerJumpState(this, stateMachine, "Jump");
        wallJumpState        = new PlayerWallJumpState(this, stateMachine, "Jump");
        airState             = new PlayerAirState(this, stateMachine, "Jump");
        dashState            = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState       = new PlayerWallSlideState(this, stateMachine, "WallSlide");


        primaryAttackState   = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        CheckForDashInput();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void SetMoveInput(InputAction.CallbackContext context)
    {
        moveInputX = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }
    }

    public void AttackTarget(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            attackInput = true;
        }
        else
        {
            attackInput = false;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dashInput = true;
        }
        else
        {
            dashInput = false;
        }
    }

    private void CheckForDashInput ()
    {

        dashUsageTimer -= Time.deltaTime;

        if (dashInput && dashUsageTimer <= 0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = moveInputX;

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);

        }
    }







}
