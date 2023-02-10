using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovementInput : MonoBehaviour
{
    private IMovementHandler _movementHandler;

    private void Awake()
    {
        _movementHandler = GetComponent<IMovementHandler>();
    }

    private void Update()
    {
        _movementHandler.MoveTowards(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
    }
}
