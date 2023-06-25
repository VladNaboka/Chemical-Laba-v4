using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PipetteController : MonoBehaviour, IDragable, IPlaceable, IInteractable
{
    [SerializeField] private GameObject _hand;
    [SerializeField] private GameObject _blob;
    [SerializeField] private Transform _spawnPosition;

    public void DragObject()
    {
        transform.SetParent(_hand.transform);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DOLocalRotate(Vector3.zero, 0.3f);
    }

    public void PlaceObject(Vector3 position)
    {
        transform.SetParent(null);
        transform.DOMove(position, 0.25f);
        transform.DORotate(new Vector3(0, transform.eulerAngles.y, 0), 0.3f);
    }

    public void InteractObject(Transform parent)
    {
        StartCoroutine(AnimationCourutine(parent));
    }

    private IEnumerator AnimationCourutine(Transform parent)
    {
        transform.SetParent(parent);
        transform.DOLocalRotate(Vector3.zero, 0.3f);
        transform.DOLocalMove(new Vector3(0,4,0), 0.3f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(_blob, _spawnPosition.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        DragObject();
    }
}
