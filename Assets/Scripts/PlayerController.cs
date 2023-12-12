using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour //скрипт передвижения игрока
{
    // переменные скрипта
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float playerSpeed = 2.0f;
    private PlayerInput _playerInput;
    private Vector3 _move;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private float _gravityValue = -9.81f;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        CheckGround();
        Move();
    }

    private void Move() //метод передвижения игрока
    {
        _move = transform.right * _playerInput.Horizontal; //реализация передвижения только по горизонтальным направлениям

        _characterController.Move(_move * playerSpeed * Time.deltaTime); //использование CharacterController для передвижения
        
        _playerVelocity.y += _gravityValue * Time.deltaTime; //гравитация игрока
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void CheckGround() //проверка на поверхность на котором стоит игрок
    {
        _groundedPlayer = _characterController.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
    }
}
