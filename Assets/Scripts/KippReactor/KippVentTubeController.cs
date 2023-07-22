using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KippVentTubeController : MonoBehaviour, IDragable, IPlaceable, IConnectable
{
    [SerializeField] private GameObject _hand;
    [SerializeField] private GameObject _vent;
    [SerializeField] private float _placeYPosition;
    private KippReactorFlaskController _kippReactorFlaskController;
    private bool _isConnected = false;
    public float PlaceYPosition => _placeYPosition;
    public bool IsConnected => _isConnected;

    public event Action<KippReactorFlaskController> onVentTubeConnected;

    public void DragObject(float dragSpeed)
    {
        transform.DOKill();
        transform.SetParent(_hand.transform);
        transform.DOLocalMove(Vector3.zero, dragSpeed);
        transform.DOLocalRotate(Vector3.zero, dragSpeed);
    }

    public void PlaceObject(Vector3 position, float dropSpeed)
    {
        transform.DOKill();
        transform.SetParent(null);
        transform.DOMove(position, dropSpeed);
        transform.DORotate(Vector3.zero, dropSpeed);
    }

    public void ConnectObject(RaycastHit hitInfo)
    {
        transform.DOKill();

        if(hitInfo.collider.GetComponent<KippReactorFlaskController>())
        StartCoroutine(ConnectToKippCoroutine(hitInfo));
    }

    private IEnumerator ConnectToKippCoroutine(RaycastHit hitInfo)
    {
        _kippReactorFlaskController = hitInfo.collider.GetComponent<KippReactorFlaskController>();
        _kippReactorFlaskController.AssignVentTube(this);
        _isConnected = true;

        GameObject connectPosition = _kippReactorFlaskController.VentTubeConnectPosition;
        onVentTubeConnected?.Invoke(_kippReactorFlaskController);

        transform.SetParent(connectPosition.transform);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DORotate(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
    }
}
