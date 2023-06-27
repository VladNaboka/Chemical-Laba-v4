using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlaskController : MonoBehaviour, IDragable, IPlaceable, IInteractable
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
        transform.DORotate(new Vector3(0, transform.eulerAngles.y, 0), dropSpeed);
    }

    public void InteractObject(Transform parent, RaycastHit hitInfo)
    {
        transform.DOKill();
        StartCoroutine(PouringAnimationCourutine(parent));
    }

    private IEnumerator PouringAnimationCourutine(Transform parent)
    {
        transform.SetParent(parent);
        transform.DOLocalRotate(new Vector3(0, transform.eulerAngles.y, 105), 0.3f);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.8f);
        DragObject(0.2f);
    }
}
