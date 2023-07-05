using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnitySimpleLiquid;

public class FlaskController : MonoBehaviour, IDragable, IPlaceable, IInteractable
{
    [SerializeField] private LiquidContainer _liquidContainer;
    [SerializeField] private ElementContainer _elementContainer;
    [SerializeField] private GameObject _hand;
    [SerializeField] private float _pouringDistance;
    [SerializeField] private float _placeYPosition;
    private float _interactDelay = 1f;
    public float PlaceYPosition => _placeYPosition;
    public float InteractDelay => _interactDelay;

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
        transform.DORotate(new Vector3(0, 0, 0), dropSpeed);
    }

    public void InteractObject(Transform parent, RaycastHit hitInfo)
    {
        transform.DOKill();

        if(hitInfo.collider.GetComponent<FlaskController>())
        StartCoroutine(PouringAnotherFlaskCoroutine(parent, hitInfo));

        if(hitInfo.collider.GetComponent<TapController>())
        StartCoroutine(FillingWaterCoroutine(parent, hitInfo));
    }

    private IEnumerator PouringAnotherFlaskCoroutine(Transform parent, RaycastHit hitInfo)
    {
        LiquidContainer interactedFlaskLiquidContainer = hitInfo.collider.GetComponent<LiquidContainer>();

        transform.SetParent(parent);
        transform.DOLocalMove(transform.right * _pouringDistance, 0.3f);
        transform.DORotate(new Vector3(0, transform.eulerAngles.y, 105), 0.3f);
        yield return new WaitForSeconds(0.3f);
        _liquidContainer.IsOpen = true;
        interactedFlaskLiquidContainer.IsOpen = true;
        yield return new WaitForSeconds(0.8f);
        _liquidContainer.IsOpen = false;
        interactedFlaskLiquidContainer.IsOpen = false;
        DragObject(0.2f);
    }

    private IEnumerator FillingWaterCoroutine(Transform parent, RaycastHit hitInfo)
    {
        LiquidContainer interactedFlaskLiquidContainer = hitInfo.collider.GetComponentInChildren<LiquidContainer>();
        _elementContainer.AddElement(ElementsList.H2O, 1);

        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DORotate(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        _liquidContainer.IsOpen = true;
        interactedFlaskLiquidContainer.IsOpen = true;
        yield return new WaitForSeconds(0.8f);
        _liquidContainer.IsOpen = false;
        interactedFlaskLiquidContainer.IsOpen = false;
        interactedFlaskLiquidContainer.FillAmount = 100;
        DragObject(0.2f);
    }
}
