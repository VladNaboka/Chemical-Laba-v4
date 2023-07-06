using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LakmussPaperController : MonoBehaviour
{
    [SerializeField] private StageScriptableObject _stageScriptableObject;
    [SerializeField] private PipetteController _pipetteController;
    [SerializeField] private Color _color;

    [Range(0f, 1f)]
    [SerializeField] private float _duration;
    private Renderer _rend;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blob"))
        {
            if(_pipetteController.ContainsRightSolution)
            {
                _stageScriptableObject._isCompleted = true;
                ChangeColor();
                _stageScriptableObject.DoStageCallback();
            }

            Destroy(collision.gameObject);
        }
    }

    private void ChangeColor()
    {
        _rend.material.DOColor(_color, _duration);
    }
}
