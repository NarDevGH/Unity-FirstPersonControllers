using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementHandler_Rigidbody : MonoBehaviour, IPlayerMovement
{
    private const ForceMode JUMP_FORCEMODE = ForceMode.Impulse;

    public float WalkSpeed
    {
        get => _walkSpeed;
    }
    [SerializeField, Min(0)] private float _walkSpeed;

    public float SprintSpeed
    {
        get => _sprintSpeed;
    }
    [SerializeField, Min(0)] private float _sprintSpeed;

    public float JumpForce
    {
        get => _jumpForce;
    }
    [SerializeField] private float _jumpForce;

    public float Mass
    {
        get => _rb.mass;
    }

    private bool _isGrounded;
    public bool IsGrounded
    {
        get => _isGrounded;
    }

    public Vector3 Velocity
    {
        get => _velocity;
    }
    private Vector3 _velocity;


    private float _targetSpeed;
    private Vector3 _moveInput;


    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _velocity = Vector3.zero;
        _targetSpeed = WalkSpeed;
    }

    private void Update()
    {
        UpdateMovement();
        HandleFreeFallVelocity();

        _velocity.Scale(new Vector3(1,0,1));
        _rb.MovePosition(transform.position + _velocity);
        _velocity.y = IsGrounded ? 0 : _rb.velocity.y;
    }

    public void MoveTowards(Vector3 dir)
    {
        if (dir.magnitude > 0)
        {
            _moveInput += transform.right * dir.x;
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
        if (IsGrounded)
        {
            _rb.AddForce(Vector3.up * JumpForce,JUMP_FORCEMODE);

            _rb.AddForce(_moveInput * JumpForce/2,JUMP_FORCEMODE);
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
        var velocity = _moveInput * _targetSpeed * Time.deltaTime;
        _velocity.x = velocity.x;
        _velocity.z = velocity.z;
    }

    private void HandleFreeFallVelocity()
    {
        _rb.velocity += Vector3.up *  (IsGrounded ? 0 : Physics.gravity.y * Mass * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
