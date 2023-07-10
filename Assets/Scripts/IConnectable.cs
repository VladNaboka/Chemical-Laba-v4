using UnityEngine;

public interface IConnectable
{
    bool IsConnected { get; }
    void ConnectObject(RaycastHit hit);
}
