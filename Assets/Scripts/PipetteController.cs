using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PipetteController : MonoBehaviour, IDragable, IPlaceable, IInteractable
{
    [SerializeField] private ElementContainer _elementContainer;
    [SerializeField] private GameObject _hand;
    [SerializeField] private GameObject _blob;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _placeYPosition;
    private float _interactDelay = 0.6f;

    public float PlaceYPosition => _placeYPosition;
    public float InteractDelay => _interactDelay;
    public bool ContainsRightSolution { get; private set; }

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
        transform.DORotate(new Vector3(0, transform.eulerAngles.y, 90), dropSpeed);
    }

    public void InteractObject(Transform parent, RaycastHit hitInfo)
    {
        transform.DOKill();

        if(hitInfo.collider.GetComponent<LakmussPaperController>())
        StartCoroutine(DropBlobCourutine(parent));

        if(hitInfo.collider.GetComponent<FlaskController>())
        StartCoroutine(FillPipetteCoroutine(parent));
    }

    private IEnumerator DropBlobCourutine(Transform parent)
    {
        transform.SetParent(parent);
        transform.DOLocalRotate(Vector3.zero, 0.3f);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        GameObject blob = Instantiate(_blob, _spawnPosition.position, Quaternion.identity);
        Destroy(blob, 0.5f);
        yield return new WaitForSeconds(0.1f);
        DragObject(0.2f);
    }

    private IEnumerator FillPipetteCoroutine(Transform parent)
    {
        ContainsRightSolution = _elementContainer.IsSolutionDone();
        Debug.Log("БУХРА: " + ContainsRightSolution);

        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.DOLocalMove(new Vector3(0, -0.5f, 0), 0.1f).OnComplete(() => transform.DOLocalMove(Vector3.zero, 0.1f));
        yield return new WaitForSeconds(0.4f);
        DragObject(0.2f);
    }
}
