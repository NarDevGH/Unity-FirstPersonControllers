using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementHandler 
{
    public Vector3 Velocity { get; }
    public void MoveTowards(Vector3 dir);
}
