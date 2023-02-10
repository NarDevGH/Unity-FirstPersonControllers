using UnityEngine;

public interface IPlayerMovement : IMovementHandler
{
    public float WalkSpeed { get; }
    public float SprintSpeed { get; }
    public float Mass { get; }
    public bool IsGrounded { get; }
    public void Jump();
    public void StartSprint();
    public void CancelSprint();
}