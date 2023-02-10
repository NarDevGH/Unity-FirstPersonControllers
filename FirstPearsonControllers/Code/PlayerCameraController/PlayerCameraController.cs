using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField, Min(0)] private float _mouseSesnsitivity = 100f;

    [SerializeField] private Transform _playerBody;
    [SerializeField] private bool _lockCursor = true;

    //private float _mouseX, _mouseY;
    private float _xRotation;
    private Vector2 _rotation;

    private void Start()
    {
        _xRotation = 0;

        if(_lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        RotateHead();
        RotateBody();
    }
    public void RotateCamera(Vector2 axisValue)
    {
        //_mouseX = axisValue.x * _mouseSesnsitivity * Time.deltaTime;
        //_mouseY = axisValue.y * _mouseSesnsitivity * Time.deltaTime;

        _rotation.x = axisValue.x * _mouseSesnsitivity * Time.deltaTime;
        _rotation.y = axisValue.y * _mouseSesnsitivity * Time.deltaTime;
    }

    private void RotateHead()
    {
        //_xRotation -= _mouseY;
        //_xRotation = Mathf.Clamp(_xRotation, -90, 90);
        //transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        _xRotation -= _rotation.y;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }

    private void RotateBody()
    {
        if (_playerBody is null) return;

        //_playerBody.Rotate(Vector3.up * _mouseX);
        _playerBody.Rotate(Vector3.up * _rotation.x);
    }

}
