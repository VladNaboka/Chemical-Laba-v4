using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private LayerMask _dropableLayer;
    private IDragable _dragable;
    private bool _isDragging = false;
    private bool _equipped = false;

    private void Update()
    {
        if(CanDrag())
        {
            Drag();
        }

        if(CanDrop())
        {
            Drop();
        }
    }

    private bool CanDrag()
    {
        return Input.GetMouseButtonDown(0) && !_isDragging && _equipped == false;
    }

    private bool CanDrop()
    {
        return Input.GetMouseButtonDown(0) && _isDragging && _equipped == true;
    }

    private void Drag()
    {
        Ray ray = new Ray
        {
            origin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)),
            direction = Camera.main.transform.forward
        };

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            _dragable = hit.collider.GetComponent<IDragable>();
            if(_dragable != null)
            {
                _dragable.DragObject();
                StartCoroutine(DragDelayCoroutine());
                _equipped = true;
            }
        }
    }

    private void Drop()
    {
        Ray ray = new Ray
        {
            origin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)),
            direction = Camera.main.transform.forward
        };

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _dropableLayer))
        {
            if (_dragable != null && _isDragging == true)
            {
                _dragable.DropObject(hit.point);
                StartCoroutine(DragDelayCoroutine());
                _equipped = false;
            }
        }
    }

    private IEnumerator DragDelayCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        _isDragging = !_isDragging;
    }
}
