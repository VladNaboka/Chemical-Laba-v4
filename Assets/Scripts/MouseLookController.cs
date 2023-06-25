using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _mouseSensitivity = 1000f;
    private float _xRotation;
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
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * _mouseInput.x);
    }
}
