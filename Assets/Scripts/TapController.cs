using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySimpleLiquid;

public class TapController : MonoBehaviour
{
    [SerializeField] private LiquidContainer _liquidContainer;

    public void OpenTap(bool isOpen)
    {
        _liquidContainer.IsOpen = isOpen;
        if(isOpen == false)
        {
            _liquidContainer.FillAmount = 100;
        }
    }
}
