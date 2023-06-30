using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnitySimpleLiquid;

public class MixingStickController : MonoBehaviour, IDragable, IPlaceable, IInteractable
{
    [SerializeField] private GameObject _hand;
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
        transform.DORotate(new Vector3(-90, 0, 0), dropSpeed);
    }

    public void InteractObject(Transform parent, RaycastHit hitInfo)
    {
        transform.DOKill();

        if(hitInfo.collider.GetComponent<FlaskController>())
        StartCoroutine(MixingCoroutine(parent, hitInfo));
    }

    private IEnumerator MixingCoroutine(Transform parent, RaycastHit hitInfo)
    {
        LiquidContainer interactedFlaskLiquidContainer = hitInfo.collider.GetComponent<LiquidContainer>();

        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DORotate(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        interactedFlaskLiquidContainer.IsOpen = true;

        yield return new WaitForSeconds(0.8f);
        interactedFlaskLiquidContainer.IsOpen = false;
        DragObject(0.2f);
    }
}
