using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GlovesController _glovesController;
    private bool _canAct = false;
    private bool _canLook = true;

    public float Horizontal { get; private set; }
    //public float Vertical { get; private set; }
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }

    private bool _isLMBClicked;
    private bool _isEkeyClicked;

    public event Action OnLMBClicked = default;
    public event Action OnEkeyClicked = default;

    private void OnEnable()
    {
        _glovesController.OnGlovesPutOn += ChangeMovementLimitations;
    }

    private void OnDisable()
    {
        _glovesController.OnGlovesPutOn -= ChangeMovementLimitations;
    }

    private void Update()
    {
        if(_canAct)
        {
            Horizontal = Input.GetAxis("Horizontal");
            //Vertical = Input.GetAxis("Vertical");
            _isLMBClicked = Input.GetMouseButtonDown(0);
        }

        if(_canLook)
        {
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");
        }

        _isEkeyClicked = Input.GetKeyDown(KeyCode.E);

        if(_isLMBClicked)
        {
            OnLMBClicked?.Invoke();
        }

        if(_isEkeyClicked)
        {
            OnEkeyClicked?.Invoke();
        }
    }

    private void ChangeMovementLimitations(bool canAct)
    {
        _canAct = canAct;
    }

    private void ChangeLookLimitations(bool canLook)
    {
        _canLook = canLook;
    }
}
