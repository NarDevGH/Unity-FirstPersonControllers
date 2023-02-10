using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour
{
    private IPlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<IPlayerMovement>();
    }

    private void Update()
    {
        _playerMovement.MoveTowards(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            _playerMovement.Jump();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)) { _playerMovement.StartSprint(); }
        else if(Input.GetKeyUp(KeyCode.LeftShift)) { _playerMovement.CancelSprint(); }
    }
}
