using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaterController : MonoBehaviour
{
    [SerializeField] private GameObject _fireParticle;

    public void EnableParticle(bool isActive)
    {
        _fireParticle.SetActive(isActive);
    }
}
