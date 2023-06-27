using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLevels : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelLocks;
    private int unlockedLevel = 1;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("UnlockedLevels"))
            unlockedLevel = PlayerPrefs.GetInt("UnlockedLevels");
        unlockedLevel =- 1;

    }

    public void UnlockLevel(int levelNum)
    {
        
    }
    public void NextLevel()
    {

    }
}
