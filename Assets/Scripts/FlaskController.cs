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
        transform.DOMove(_hand.transform.position, 0.3f).OnComplete(() => transform.SetParent(_hand.transform));
    }

    public void DropObject(Vector3 position)
    {
        transform.SetParent(null);
        transform.DOMove(position, 0.3f);
        transform.DORotate(Vector3.zero, 0.3f);
    }
}
