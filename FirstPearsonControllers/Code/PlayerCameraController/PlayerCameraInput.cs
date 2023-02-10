using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCameraController))]
public class PlayerCameraInput : MonoBehaviour
{
    private PlayerCameraController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerCameraController>();
    }

    private void Update()
    {
        _controller.RotateCamera(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))); 
    }
}
