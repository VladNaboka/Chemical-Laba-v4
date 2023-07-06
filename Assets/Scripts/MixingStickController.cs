using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnitySimpleLiquid;

public class MixingStickController : MonoBehaviour, IDragable, IPlaceable, IInteractable
{
    [SerializeField] private GameObject _hand;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _placeYPosition;
    private Animator _anim;
    private float _interactDelay = 1.5f;
    public float PlaceYPosition => _placeYPosition;
    public float InteractDelay => _interactDelay;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

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
        ElementContainer elementContainer = hitInfo.collider.GetComponent<ElementContainer>();

        elementContainer.MixElements(true);

        transform.SetParent(parent);
        transform.DOLocalMove(new Vector3(0, -0.35f, 0), 0.3f);
        transform.DORotate(Vector3.zero, 0.3f);
        interactedFlaskLiquidContainer.IsOpen = true;
        yield return new WaitForSeconds(0.3f);
        _animator.enabled = true;
        _anim.Play("MixingAnimation");
        yield return new WaitForSeconds(0.8f);
        _animator.enabled = false;
        interactedFlaskLiquidContainer.IsOpen = false;
        DragObject(0.2f);
    }
}
