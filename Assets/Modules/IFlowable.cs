using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlowable
{
    int Amount { get; }
    ElementsList GetElement();
    void OpenTap(bool isOpen);
}
