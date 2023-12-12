using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KippReactorFlaskController : MonoBehaviour, IPoorable
{
    [SerializeField] private ElementContainer _elementContainer;
    private KippVesselController _kippVesselController;
    private KippVentTubeController _kippVentTubeController;
    private ElementContainer _vesselElementContainer;
    private bool _vesselConnected = false;
    private bool _ventTubeConnected = false;
    private bool _allPartsConnected = false;
    [field: SerializeField] public GameObject VesselConnectPosition { get; private set; }
    [field: SerializeField] public GameObject VentTubeConnectPosition { get; private set; }

    public void AssignVessel(KippVesselController kippVesselController, ElementContainer vesselElementContainer)
    {
        _kippVesselController = kippVesselController;
        _vesselElementContainer = vesselElementContainer;

        if(_kippVesselController != null)
        {
            _vesselConnected = true;
        }
    }

    public void AssignVentTube(KippVentTubeController kippVentTubeController)
    {
        _kippVentTubeController = kippVentTubeController;

        if(kippVentTubeController != null)
        {
            _ventTubeConnected = true;
        }
    }

    public bool IsAllPartsDone()
    {
        return _vesselConnected && _ventTubeConnected && _elementContainer.IsSolutionDone() && _vesselElementContainer.IsSolutionDone();
    }
}
