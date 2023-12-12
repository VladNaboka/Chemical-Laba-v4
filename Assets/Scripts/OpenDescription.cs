using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDescription : MonoBehaviour //���������� �������� �������� �����
{
    //����������
    [SerializeField] private GameObject _description;
    private bool _isOpened = false; // ��������� �������� ������� ����������

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // �������� �� ������� ������ Q ������ ������������
        {
            if (_isOpened == false) // ���� ���������� ����� false, �� ������ _description ���������� ��������
            {
                _isOpened = true;
                _description.SetActive(true);
            }
            else if (_isOpened == true) // ���� ���������� ����� true, �� ������ _description ���������� ����������
            {
                _isOpened = false;
                _description.SetActive(false);
            }
            Debug.Log(_isOpened);
        }
    }
}
