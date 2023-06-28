using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    private void Move()
    {
        _move = transform.right * _playerInput.Horizontal;

        _characterController.Move(_move * playerSpeed * Time.deltaTime);
        
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void CheckGround()
    {
        _groundedPlayer = _characterController.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
    }
}
