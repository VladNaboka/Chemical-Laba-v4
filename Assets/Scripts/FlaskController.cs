using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlaskController : MonoBehaviour, IDragable, IPlaceable
{
    [SerializeField] private GameObject _hand;

    public void DragObject()
    {
        transform.SetParent(_hand.transform);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        transform.DOLocalRotate(Vector3.zero, 0.3f);
    }

    public void PlaceObject(Vector3 position)
    {
        transform.SetParent(null);
        transform.DOMove(position, 0.3f);
        transform.DORotate(new Vector3(0, transform.eulerAngles.y, 0), 0.3f);
    }
}
