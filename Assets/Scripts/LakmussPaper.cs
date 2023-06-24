using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakmussPaper : MonoBehaviour
{
    [SerializeField] private Color _color;

    [Range(0f, 1f)]
    [SerializeField] private float _duration;
    private Renderer _rend;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        ChangeColor();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blob"))
        {
            ChangeColor();
            Destroy(collision.gameObject);
        }
    }

    private void ChangeColor()
    {
        _rend.material.DOColor(_color, _duration);
    }
}
