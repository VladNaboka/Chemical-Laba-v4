using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/Stage")]
public class StageScriptableObject : ScriptableObject
{
    [SerializeField] private string _stageName;
    public bool isCompleted;
    public event Action onStageCompleted;

    public void SetDefaultValues()
    {
        isCompleted = false;
    }

    public void DoStageCallback()
    {
        onStageCompleted?.Invoke();
    }
}
