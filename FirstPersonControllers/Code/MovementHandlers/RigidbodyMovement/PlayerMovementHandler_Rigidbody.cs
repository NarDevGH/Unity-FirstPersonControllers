using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementHandler_Rigidbody : MonoBehaviour, IPlayerMovementHandler
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

    private bool _isGrounded;
    public bool IsGrounded
    {
        get => _isGrounded;
    }

    public float Mass
    {
        get => _rb.mass;
    }
    public Vector3 Velocity
    {
        get => new Vector3(_moveInput.x, _rb.velocity.y, _moveInput.z);
    }


    private float _targetSpeed;
    private Vector3 _moveInput;

    private Rigidbody _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _targetSpeed = WalkSpeed;
    }

    private void Update()
    {
        UpdateMovement();
        HandleFreeFallVelocity();
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
        _rb.MovePosition(transform.position + (_moveInput * _targetSpeed * Time.deltaTime) );
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
