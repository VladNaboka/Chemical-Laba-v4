using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGazable
{
    bool IsGazReady { get; }
    int Amount { get; }
    ElementsList GetElement();
}
