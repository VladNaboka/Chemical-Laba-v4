using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //импорт библиотеки для работы со сценами

public class SceneController : MonoBehaviour
{

    private void Update() //метод Unity который срабатывает каждую миллисекунду 
    {
        if (Input.GetKeyDown(KeyCode.R)) //идет проверка на нажатие кнопки R (Рестарт) 
        {
            Restart(); //метод где прописан сам рестарт
        }
    }

    public void LoadOnBtnClick(string name) // метод который добавляется в кнопку при нажатии которой можно будет переключаться между сценами вбивая название сценв в параметр name
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single); //загрузка сцены
    }
    
    public void Restart() //метод рестарта
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, //берем индекс текущей сцены
            LoadSceneMode.Single);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single); // загрузка самой первой сцены
    }
}
