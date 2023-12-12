using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySimpleLiquid;
using DG.Tweening;

public class GazFlaskController : MonoBehaviour, IDragable, IPlaceable, IInteractable, IPoorable
{
    [SerializeField] private StageScriptableObject _stageScriptableObject;
    [SerializeField] private LiquidContainer _liquidContainer;
    [SerializeField] private ElementContainer _elementContainer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _hand;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _placeYPosition;
    private float _interactDelay = 1.5f;
    public float PlaceYPosition => _placeYPosition;
    public float InteractDelay => _interactDelay;

    public void DragObject(float dragSpeed)
    {
        transform.DOKill();
        transform.SetParent(_hand.transform);
        transform.DOLocalMove(Vector3.zero, dragSpeed);
        transform.DOLocalRotate(_rotation, dragSpeed);
    }

    public void PlaceObject(Vector3 position, float dropSpeed)
    {
        transform.DOKill();
        transform.SetParent(null);
        transform.DOMove(position, dropSpeed);
        transform.DORotate(_rotation, dropSpeed);
    }

    public void InteractObject(Transform parent, RaycastHit hitInfo)
    {
        transform.DOKill();

        if(hitInfo.collider.GetComponent<IGazable>() != null)
        StartCoroutine(FillingElementCoroutine(parent, hitInfo));

        if(hitInfo.collider.GetComponent<HeaterController>())
        StartCoroutine(HeatElementCoroutine(parent, hitInfo));
    }

    private IEnumerator FillingElementCoroutine(Transform parent, RaycastHit hitInfo)
    {
        IGazable gazable = hitInfo.collider.GetComponent<IGazable>();

        if(gazable.IsGazReady)
        _elementContainer.AddElement(gazable.GetElement(), gazable.Amount);

        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DORotate(_rotation, 0.3f);
        yield return new WaitForSeconds(0.3f);

        if(gazable.IsGazReady)
        _liquidContainer.FillAmount = 100;

        yield return new WaitForSeconds(0.8f);
        DragObject(0.2f);
    }

    private IEnumerator HeatElementCoroutine(Transform parent, RaycastHit hitInfo)
    {
        HeaterController interactedHeaterController = hitInfo.collider.GetComponent<HeaterController>();
        interactedHeaterController.EnableParticle(true);

        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DORotate(_rotation, 0.3f);

        _stageScriptableObject.DoStageCallback(_elementContainer.IsSolutionDone());

        yield return new WaitForSeconds(0.4f);

        if(_elementContainer.IsSolutionDone())
            _audioSource.Play();
        
        yield return new WaitForSeconds(0.8f);
        interactedHeaterController.EnableParticle(false);

        DragObject(0.2f);
    } 
}