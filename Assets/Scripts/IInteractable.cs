using UnityEngine;

public interface IInteractable
{
    float InteractDelay { get; }
    void InteractObject(Transform parent, RaycastHit hitInfo);
}
