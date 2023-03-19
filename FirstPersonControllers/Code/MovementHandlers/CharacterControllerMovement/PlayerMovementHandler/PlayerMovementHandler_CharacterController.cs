using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementHandler_CharacterController : MonoBehaviour,IPlayerMovementHandler
{
    public float WalkSpeed
    {
        get => _walkSpeed;
    }
    [SerializeField, Min(0)] private float _walkSpeed;

    public float SprintSpeed
    {
        get => _sprintSpeed;
    }
    [SerializeField,Min(0)]private float _sprintSpeed;

    public float  JumpHeight
    {
        get => _jumpHeight; 
    }
    [SerializeField] private float  _jumpHeight;

    public float Mass
    {
        get => _mass; 
    }
    [SerializeField]private float _mass;
    
    public bool IsGrounded 
    {
        get => _controller.isGrounded;
    }

    public Vector3 Velocity
    {
        get => _velocity; 
    }
    private Vector3 _velocity;


    private float _targetSpeed;
    private Vector3 _moveInput;


    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _velocity = Vector3.zero;
        _targetSpeed = WalkSpeed;
    }

    private void Update()
    {
        UpdateMovement();
        UpdateGravity();
        Debug.Log(IsGrounded);
        _controller.Move(_velocity);
    }

    public void MoveTowards(Vector3 dir)
    {
        if(dir.magnitude > 0) 
        {
            _moveInput = transform.right * dir.x;
            _moveInput += transform.forward * dir.z;

            //Clamp to avoid faster diagonal movement.
            _moveInput = Vector3.ClampMagnitude(_moveInput, 1);
        }
        else
        {
            _moveInput = Vector3.zero;
        }

    }

    public void Jump()
    {
        if(IsGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y * _mass);
            _controller.Move(Vector3.up * _velocity.y);
        }
    }

    public void StartSprint()
    {
        _targetSpeed = SprintSpeed;
    }
    public void CancelSprint()
    {
        _targetSpeed = WalkSpeed;
    }

    private void UpdateMovement()
    {
        var moveInput = _moveInput * _targetSpeed * Time.deltaTime;
        _velocity.x = moveInput.x;
        _velocity.z = moveInput.z;
    }

    private void UpdateGravity()
    {
        if(IsGrounded)
        {
            _velocity.y = -2;
        }
        else
        {
            _velocity.y +=  Physics.gravity.y * Mass * Time.deltaTime;
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // If collide with cellign.
        if((_controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            _velocity.y = 0;
        }
    }
}
