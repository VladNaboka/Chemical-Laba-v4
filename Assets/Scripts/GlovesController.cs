using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesController : MonoBehaviour, IUsable
{
    [SerializeField] private GameObject _glovesText;
    public event Action OnGlovesPutOn = default;

    public void UseObject()
    {
        OnGlovesPutOn?.Invoke();
        _glovesText.SetActive(false);
        gameObject.SetActive(false);
    }
}