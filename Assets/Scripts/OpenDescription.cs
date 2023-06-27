using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDescription : MonoBehaviour
{
    [SerializeField] private GameObject _description;
    private bool _isOpened = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_isOpened == false)
            {
                _isOpened = true;
                _description.SetActive(true);
            }
            else if (_isOpened == true)
            {
                _isOpened = false;
                _description.SetActive(false);
            }
            Debug.Log(_isOpened);
        }
    }
}
