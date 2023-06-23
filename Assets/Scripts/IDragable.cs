using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    void DragObject();
    void DropObject(Vector3 position);
}
