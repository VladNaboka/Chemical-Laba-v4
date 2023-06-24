using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerLogic : MonoBehaviour
{
    [SerializeField] private GameObject _blob;
    [SerializeField] private Transform _spawnPosition;

    private void Update()
    {
        // Vstavit nashu System
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnBlob();
        }
    }

    private void SpawnBlob()
    {
        Instantiate(_blob, _spawnPosition.position, Quaternion.identity);
    }
}
