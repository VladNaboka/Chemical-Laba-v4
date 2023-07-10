using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnitySimpleLiquid;

public class KippKranController : MonoBehaviour, IUsable
{
    [SerializeField] private LiquidContainer _liquidContainer;
    [SerializeField] private KippVentTubeController _kippVentTubeController;
    private KippReactorFlaskController _kippReactorFlaskController;
    private bool _isOpen = false;

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

        _isOpen = !_isOpen;

        if(_isOpen)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
            CheckKippIntegrity();
        }
        else
        {
            transform.DOLocalRotate(new Vector3(0, -90, 0), 0.3f);
            CheckKippIntegrity();
        }
    }

    private void OnVentTubeConnected(KippReactorFlaskController kippReactorFlaskController)
    {
        _kippReactorFlaskController = kippReactorFlaskController;
        CheckKippIntegrity();
    }

    private void CheckKippIntegrity()
    {
        if(_kippReactorFlaskController.IsAllPartsDone())
        {
            _liquidContainer.IsOpen = _isOpen;
        }
    }
}
