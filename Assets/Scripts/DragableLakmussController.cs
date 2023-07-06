using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragableLakmussController : MonoBehaviour, IDragable, IPlaceable, IInteractable
{
    [SerializeField] private StageScriptableObject _stageScriptableObject;
    [SerializeField] private GameObject _hand;
    [SerializeField] private Color _color;

    [Range(0f, 1f)]
    [SerializeField] private float _duration;

    [SerializeField] private float _placeYPosition;
    private float _interactDelay = 1.5f;

    public float PlaceYPosition => _placeYPosition;
    public float InteractDelay => _interactDelay;
    private Renderer _rend;


    private void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    public void DragObject(float dragSpeed)
    {
        transform.DOKill();
        transform.SetParent(_hand.transform);
        transform.DOLocalMove(Vector3.zero, dragSpeed);
        transform.DOLocalRotate(new Vector3(-180, 0, 0), dragSpeed);
    }

    public void PlaceObject(Vector3 position, float dropSpeed)
    {
        transform.DOKill();
        transform.SetParent(null);
        transform.DOMove(position, dropSpeed);
        transform.DORotate(new Vector3(-90, transform.eulerAngles.y, 0), dropSpeed);
    }

    public void InteractObject(Transform parent, RaycastHit hitInfo)
    {
        transform.DOKill();

        if(hitInfo.collider.GetComponent<FlaskController>())
        StartCoroutine(DipLakmussCoroutine(parent, hitInfo));
    }

    private IEnumerator DipLakmussCoroutine(Transform parent, RaycastHit hitInfo)
    {
        ElementContainer elementContainer = hitInfo.collider.GetComponent<ElementContainer>();

        transform.SetParent(parent);
        transform.DOLocalRotate(new Vector3(-180, 0, 0), 0.3f);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        transform.DOLocalMove(new Vector3(0, -0.5f, 0), 0.5f);
        if(elementContainer.IsSolutionDone())
        {
            _stageScriptableObject._isCompleted = true;
            ChangeColor();
            _stageScriptableObject.DoStageCallback();
        }
        yield return new WaitForSeconds(0.5f);
        transform.DOLocalMove(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.5f);
        DragObject(0.2f);
    }

    private void ChangeColor()
    {
        _rend.material.DOColor(_color, _duration);
    }
}
