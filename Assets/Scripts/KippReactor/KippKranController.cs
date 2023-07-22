using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnitySimpleLiquid;

public class KippKranController : MonoBehaviour, IUsable
{
    [SerializeField] private KippVentTubeController _kippVentTubeController;
    private KippReactorFlaskController _kippReactorFlaskController;
    public bool _isOpen = false;

    public event Action<bool> onKippKranUsed;

    private void OnEnable()
    {
        _kippVentTubeController.onVentTubeConnected += OnVentTubeConnected;
    }

    private void OnDisable()
    {
        _kippVentTubeController.onVentTubeConnected -= OnVentTubeConnected;
    }

    public void UseObject()
    {
        transform.DOKill();

        if(_kippReactorFlaskController != null && _kippReactorFlaskController.IsAllPartsDone())
        _isOpen = !_isOpen;

        onKippKranUsed?.Invoke(_isOpen);

        if(_isOpen)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
        }
        else
        {
            transform.DOLocalRotate(new Vector3(0, -90, 0), 0.3f);
        }
    }

    private void OnVentTubeConnected(KippReactorFlaskController kippReactorFlaskController)
    {
        _kippReactorFlaskController = kippReactorFlaskController;
    }
}
