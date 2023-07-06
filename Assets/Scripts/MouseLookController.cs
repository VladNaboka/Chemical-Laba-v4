using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _mouseSensitivity = 500f;
    private float _xRotation;
    private float _yRotation;
    private Vector2 _mouseInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        _mouseInput.x = _playerInput.MouseX * _mouseSensitivity * Time.deltaTime; 
        _mouseInput.y = _playerInput.MouseY * _mouseSensitivity * Time.deltaTime; 

        _xRotation -= _mouseInput.y;
        _xRotation = Mathf.Clamp(_xRotation, -70f, 70f);

        _yRotation += _mouseInput.x;
        _yRotation = Mathf.Clamp(_yRotation, -70f, 70f);

        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }

    private void ChangeLookLimitations(bool isLevelCompleted)
    {
        if(isLevelCompleted)
        Cursor.lockState = CursorLockMode.None;
    }
}
