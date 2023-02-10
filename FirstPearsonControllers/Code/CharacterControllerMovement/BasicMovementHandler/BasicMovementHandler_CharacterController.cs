using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicMovementHandler_CharacterController : MonoBehaviour, IMovementHandler
{
    [SerializeField] private float _moveSpeed;

    public Vector3 Velocity
    {
        get => _velocity;
    }
    private Vector3 _velocity;
    

    private Vector3 _moveInput;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _velocity = Vector3.zero;
    }

    private void Update()
    {
        UpdateMovement();

        _controller.Move(_velocity);
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

    private void UpdateMovement()
    {
        var moveInput = _moveInput * _moveSpeed * Time.deltaTime;
        _velocity.x = moveInput.x;
        _velocity.z = moveInput.z;
    }
}
