using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    ToMenu();
        //}
    }
    public void LoadOnBtnClick(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
    
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.
            GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void ToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
