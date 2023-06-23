using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlaskController : MonoBehaviour, IDragable
{
    [SerializeField] private GameObject _hand;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DragObject()
    {
        transform.SetParent(_hand.transform);
        transform.DOLocalMove(Vector3.zero, 0.25f);
        transform.DOLocalRotate(Vector3.zero, 0.25f);
    }

    public void DropObject(Vector3 position)
    {
        transform.SetParent(null);
        transform.DOMove(position, 0.25f);
        transform.DORotate(new Vector3(0, transform.eulerAngles.y, 0), 0.25f);
    }
}
