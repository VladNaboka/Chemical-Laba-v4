using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnitySimpleLiquid;

public class FlaskController : MonoBehaviour, IDragable, IPlaceable, IInteractable
{
    [SerializeField] private StageScriptableObject _stageScriptableObject;
    [SerializeField] private LiquidContainer _liquidContainer;
    [SerializeField] private ElementContainer _elementContainer;
    [SerializeField] private GameObject _hand;
    [SerializeField] private float _placeYPosition;
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

        if(hitInfo.collider.GetComponent<TapController>())
        StartCoroutine(FillingElementCoroutine(parent, hitInfo, ElementsList.H2O));

        if(hitInfo.collider.GetComponent<HeaterController>())
        StartCoroutine(HeatElementCoroutine(parent, hitInfo));
    }

    private IEnumerator FillingElementCoroutine(Transform parent, RaycastHit hitInfo, ElementsList element)
    {
        TapController interactedTapController = hitInfo.collider.GetComponent<TapController>();
        _elementContainer.AddElement(element, 1);

        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DORotate(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        _liquidContainer.IsOpen = true;
        interactedTapController.OpenTap(true);
        yield return new WaitForSeconds(0.8f);
        _liquidContainer.IsOpen = false;
        interactedTapController.OpenTap(false);
        DragObject(0.2f);
    }

    private IEnumerator HeatElementCoroutine(Transform parent, RaycastHit hitInfo)
    {
        HeaterController interactedHeaterController = hitInfo.collider.GetComponent<HeaterController>();
        interactedHeaterController.EnableParticle(true);

        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DORotate(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);

        if(_elementContainer.IsSolutionDone())
        {
            _elementContainer.HeatElements(true);

        }
        else
        _elementContainer.HeatElements(false);

        yield return new WaitForSeconds(0.8f);
        interactedHeaterController.EnableParticle(false);
        DragObject(0.2f);
    } 
}
