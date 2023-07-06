using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    [SerializeField] private List<StageScriptableObject> _stagesScriptableObjects = new List<StageScriptableObject>();
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private int _nextLevelIndex;

    public event Action<bool> onLevelCompleted;

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
            PlayerPrefs.SetInt("CompletedLevels", _nextLevelIndex);
            StartCoroutine(SetWinDelay());
        }
    }

    private IEnumerator SetWinDelay()
    {
        yield return new WaitForSeconds(1.5f);
        _winPanel.SetActive(true);
        onLevelCompleted?.Invoke(true);
    }
}
