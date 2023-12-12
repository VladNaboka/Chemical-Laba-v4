using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySimpleLiquid;

public class TapController : MonoBehaviour, IFlowable
{
    [SerializeField] private LiquidContainer _liquidContainer;
    [SerializeField] private int _amount;
    [SerializeField] private ElementsList _element;

    public int Amount => _amount;
    
    public void OpenTap(bool isOpen)
    {
        _liquidContainer.IsOpen = isOpen;
        if(isOpen == false)
        {
            _liquidContainer.FillAmount = 100;
        }
    }

    public ElementsList GetElement()
    {
        return _element;
    }
}
