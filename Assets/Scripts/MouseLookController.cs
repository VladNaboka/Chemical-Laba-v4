using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookController : MonoBehaviour //скрипт вращени€ камеры
{
    // обозначение переменных скрипта
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _mouseSensitivity = 500f;
    private float _xRotation;
    private float _yRotation;
    private Vector2 _mouseInput;

    private void Start() // начинаетс€ в первую секунду запуска сцены
    {
        Cursor.lockState = CursorLockMode.Locked; // скрываем курсор на сцене
    }

    private void Update()
    {
        Look();
    }

    private void Look() // метод вращени€ камеры
    {
        _mouseInput.x = _playerInput.MouseX * _mouseSensitivity; 
        _mouseInput.y = _playerInput.MouseY * _mouseSensitivity;  //использование playerInput дл€ реализации вращени€ камерой

        _xRotation -= _mouseInput.y;
        _xRotation = Mathf.Clamp(_xRotation, -70f, 70f); // ограничени€ камеры по Y

        _yRotation += _mouseInput.x;
        _yRotation = Mathf.Clamp(_yRotation, -70f, 70f); // ограничени€ камеры по X

        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f); //реализаци€ движени€ камеры через Quaternion
    }

    private void ChangeLookLimitations(bool isLevelCompleted)
    {
        if(isLevelCompleted)
        Cursor.lockState = CursorLockMode.None;
    }
}
