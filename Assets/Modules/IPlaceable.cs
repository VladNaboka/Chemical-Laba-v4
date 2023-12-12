using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable
{
    float PlaceYPosition { get; }
    void PlaceObject(Vector3 position, float dropSpeed);
}
