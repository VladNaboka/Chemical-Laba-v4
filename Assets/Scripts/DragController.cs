using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private LayerMask _dropableLayer;
    [SerializeField] private LayerMask _interactableLayer;
    private IDragable _dragable;
    private IPlaceable _placeable;
    private IInteractable _interactable;
    private Ray _ray;
    private RaycastHit _hitInfo;
    private float _dragAndDropDelay = 0.3f;
    private float _interactDelay;
    private float _animationSpeed = 0.3f;
    private bool _isDragging = false;
    private bool _equipped = false;
    private bool _canInteract = false;

    private void OnEnable()
    {
        _playerInput.OnLMBClicked += Drag;
        _playerInput.OnLMBClicked += Place;
        _playerInput.OnEkeyClicked += Interact;
        _playerInput.OnEkeyClicked += Use;
    }

    private void OnDisable()
    {
        _playerInput.OnLMBClicked -= Drag;
        _playerInput.OnLMBClicked -= Place;
        _playerInput.OnEkeyClicked -= Interact;
        _playerInput.OnEkeyClicked -= Use;
    }

    private void Update()
    {
        CreateRay();
    }

    private void CreateRay()
    {
        _ray = new Ray
        {
            origin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)),
            direction = Camera.main.transform.forward
        };
    }

    private void Drag()
    {
        if(!_isDragging && !_equipped)
        if(Physics.Raycast(_ray, out _hitInfo))
        {
            _dragable = _hitInfo.collider.GetComponent<IDragable>();
            _placeable = _dragable as IPlaceable;
            _interactable = _dragable as IInteractable;

            if(_dragable != null)
            {
                _dragable.DragObject(_animationSpeed);
                StartCoroutine(DragDelayCoroutine());
                _equipped = true;
                _canInteract = true;
            }
        }
    }

    private void Place()
    {
        if(_isDragging && _equipped && _canInteract)
        if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, _dropableLayer))
        {
            if (_placeable != null && _hitInfo.normal == Vector3.up)
            {
                _placeable.PlaceObject(new Vector3(_hitInfo.point.x, _hitInfo.point.y + _placeable.PlaceYPosition, _hitInfo.point.z), _animationSpeed);
                StartCoroutine(DragDelayCoroutine());
                _equipped = false;
                _canInteract = false;
            }
        }
    }

    private void Interact()
    {
        if(_equipped && _canInteract)
        if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, _interactableLayer))
        {
            if(_interactable != null)
            {
                Transform childTransform = _hitInfo.collider.transform.GetChild(0);
                _interactDelay = _interactable.InteractDelay;
                _interactable.InteractObject(childTransform, _hitInfo);
                StartCoroutine(InteractDelayCoroutine());
                _canInteract = false;
            }
        }
    }

    private void Use()
    {
        if(Physics.Raycast(_ray, out _hitInfo))
        {
            IUsable usable = _hitInfo.collider.GetComponent<IUsable>();

            if(usable != null)
            {
                usable.UseObject();
            }
        }
    }

    private IEnumerator DragDelayCoroutine()
    {
        yield return new WaitForSeconds(_dragAndDropDelay);
        _isDragging = !_isDragging;
    }

    private IEnumerator InteractDelayCoroutine()
    {
        yield return new WaitForSeconds(_interactDelay);
        _canInteract = true;
    }
}
