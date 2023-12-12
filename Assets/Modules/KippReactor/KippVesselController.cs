using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KippVesselController : MonoBehaviour, IDragable, IPlaceable, IConnectable, IPoorable
{
    [SerializeField] ElementContainer _elementContainer;
    [SerializeField] private GameObject _hand;
    [SerializeField] private float _placeYPosition;
    private bool _isFilled = false;
    private bool _isConnected = false;
    public float PlaceYPosition => _placeYPosition;
    public bool IsConnected => _isConnected;

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
        transform.DORotate(new Vector3(83, -90, 0), dropSpeed);
    }

    public void ConnectObject(RaycastHit hitInfo)
    {
        transform.DOKill();

        if(hitInfo.collider.GetComponent<KippReactorFlaskController>())
        StartCoroutine(ConnectToKippCoroutine(hitInfo));
    }

    private IEnumerator ConnectToKippCoroutine(RaycastHit hitInfo)
    {
        KippReactorFlaskController kippReactorFlaskController = hitInfo.collider.GetComponent<KippReactorFlaskController>();
        kippReactorFlaskController.AssignVessel(this, _elementContainer);
        _isConnected = true;

        GameObject connectPosition = kippReactorFlaskController.VesselConnectPosition;

        transform.SetParent(connectPosition.transform);
        transform.DOLocalMove(new Vector3(0, 3.5f, 0), 0.3f).OnComplete(() => transform.DOLocalMove(Vector3.zero, 0.3f));
        transform.DORotate(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.6f);
    }
}
