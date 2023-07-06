using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletnessManager : MonoBehaviour
{
    [SerializeField] private List<StageScriptableObject> _stagesScriptableObjects = new List<StageScriptableObject>();

    private void Awake()
    {
        foreach (StageScriptableObject stage in _stagesScriptableObjects)
        {
            stage.SetDefaultValues();
        }
    }

    private void OnEnable()
    {
        foreach (StageScriptableObject stage in _stagesScriptableObjects)
        {
            stage.onStageCompleted += CheckLevelCompletness;
        }
    }

    private void OnDisable()
    {
        foreach (StageScriptableObject stage in _stagesScriptableObjects)
        {
            stage.onStageCompleted -= CheckLevelCompletness;
        }
    }

    private void CheckLevelCompletness()
    {
        bool isLevelCompleted = _stagesScriptableObjects.TrueForAll(x => x._isCompleted);
        if(isLevelCompleted)
        {
            Debug.Log("УРОВЕНЬ ПРОЙДЕН");
        }
    }
}
