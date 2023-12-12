using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySimpleLiquid;

public class KippLiquidOutletController : MonoBehaviour, IGazable
{
    [SerializeField] private LiquidContainer _liquidContainer;
    [SerializeField] private KippVentTubeController _kippVentTubeController;
    [SerializeField] private KippKranController _kippKranController;
    [SerializeField] private int _amount;
    [SerializeField] private ElementsList _element;
    private KippReactorFlaskController _kippReactorFlaskController;
    private bool _isKranOpen;
    private bool _isGazReady;

    public int Amount => _amount;
    public bool IsGazReady => _isGazReady;

    private void OnEnable()
    {
        _kippVentTubeController.onVentTubeConnected += OnVentTubeConnected;
        _kippKranController.onKippKranUsed += CheckKran;
    }

    private void OnDisable()
    {
        _kippVentTubeController.onVentTubeConnected -= OnVentTubeConnected;
        _kippKranController.onKippKranUsed -= CheckKran;
    }

    private void Update()
    {
        _liquidContainer.FillAmount = 100;
    }

    public ElementsList GetElement()
    {
        return _element;
    }

    private void OnVentTubeConnected(KippReactorFlaskController kippReactorFlaskController)
    {
        _kippReactorFlaskController = kippReactorFlaskController;
        CheckKran(_isKranOpen);
    }

    private void CheckKran(bool isKranOpen)
    {
        _isKranOpen = isKranOpen;
        Open();
    }

    public void Open()
    {
        if(_kippReactorFlaskController != null && _kippReactorFlaskController.IsAllPartsDone())
        {
            _liquidContainer.IsOpen = _isKranOpen;
            _isGazReady = _isKranOpen;
        }
    }
}
