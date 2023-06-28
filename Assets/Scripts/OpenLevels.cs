using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLevels : MonoBehaviour
{
    public static OpenLevels instance;

    private void Start()
    {
        if (instance == null)
        { 
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    ///<summary>
    ///Input here number of level you want to ulock. It will save in PlayerPrefs. Example: UnlockLevel(2) - unlock second level.
    ///</summary>
    public void UnlockLevel(int levelNum)
    {
        PlayerPrefs.SetInt("CompletedLevels", levelNum);
    }
}
