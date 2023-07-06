using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLevels : MonoBehaviour
{
    [SerializeField] private GameObject[] _lockLevel;
    private int _level = 1;
    public static int currentLevelStatic;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("CompletedLevels"))
        {
            _level = PlayerPrefs.GetInt("CompletedLevels");
        }
        InitUnlockedLevels();
    }

    private void InitUnlockedLevels()
    {
        for(int i = 0; i < _level; i++)
        {
            _lockLevel[i].SetActive(false); 
        }
    }
}
