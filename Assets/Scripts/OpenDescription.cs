using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDescription : MonoBehaviour //реализация открытия описания опыта
{
    //переменные
    [SerializeField] private GameObject _description;
    private bool _isOpened = false; // начальное значение булевой переменной

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // проверка на нажатие кнопки Q каждую миллисекунду
        {
            if (_isOpened == false) // если переменная равна false, то объект _description становится активным
            {
                _isOpened = true;
                _description.SetActive(true);
            }
            else if (_isOpened == true) // если переменная равна true, то объект _description становится неактивным
            {
                _isOpened = false;
                _description.SetActive(false);
            }
            Debug.Log(_isOpened);
        }
    }
}
