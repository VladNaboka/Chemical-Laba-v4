using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnitySimpleLiquid;

public class TubeController : MonoBehaviour, IDragable, IPlaceable, IInteractable
{
    [SerializeField] private LiquidContainer _liquidContainer;
    [SerializeField] private GameObject _hand;
    [SerializeField] private ElementsList _elementType;
    [SerializeField] private float _pouringDistance;
    [SerializeField] private float _placeYPosition;
    private int _elementAmount = 1;
    private float _interactDelay = 1.5f;
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
        StartCoroutine(PouringCourutine(parent, hitInfo));

        if(hitInfo.collider.GetComponent<KippReactorFlaskController>())
        StartCoroutine(PouringApparatusCourutine(parent, hitInfo));
    }

    private IEnumerator PouringCourutine(Transform parent, RaycastHit hitInfo)
    {
        LiquidContainer interactedFlaskLiquidContainer = hitInfo.collider.GetComponent<LiquidContainer>();
        ElementContainer elementContainer = hitInfo.collider.GetComponent<ElementContainer>();

        if(_liquidContainer.FillAmount > 0)
        {
            elementContainer.AddElement(_elementType, _elementAmount);
        }

        transform.SetParent(parent);
        transform.DOLocalMove(transform.right * _pouringDistance, 0.3f);
        transform.DORotate(new Vector3(0, transform.eulerAngles.y, 105), 0.3f);
        yield return new WaitForSeconds(0.2f);
        _liquidContainer.IsOpen = true;
        interactedFlaskLiquidContainer.IsOpen = true;
        yield return new WaitForSeconds(0.8f);
        _liquidContainer.IsOpen = false;
        interactedFlaskLiquidContainer.IsOpen = false;
        DragObject(0.2f);
    }

    private IEnumerator PouringApparatusCourutine(Transform parent, RaycastHit hitInfo)
    {
        LiquidContainer interactedFlaskLiquidContainer = hitInfo.collider.GetComponent<LiquidContainer>();
        ElementContainer elementContainer = hitInfo.collider.GetComponent<ElementContainer>();

        if(_liquidContainer.FillAmount > 0)
        {
            elementContainer.AddElement(_elementType, _elementAmount);
        }

        transform.SetParent(parent);
        transform.DOLocalMove(transform.right * _pouringDistance, 0.3f);
        transform.DORotate(new Vector3(0, 0, 105), 0.3f);
        yield return new WaitForSeconds(0.2f);
        _liquidContainer.IsOpen = true;
        interactedFlaskLiquidContainer.IsOpen = true;
        yield return new WaitForSeconds(0.8f);
        _liquidContainer.IsOpen = false;
        interactedFlaskLiquidContainer.IsOpen = false;
        DragObject(0.2f);
    }
}
