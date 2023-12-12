using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //������ ���������� ��� ������ �� �������

public class SceneController : MonoBehaviour
{

    private void Update() //����� Unity ������� ����������� ������ ������������ 
    {
        if (Input.GetKeyDown(KeyCode.R)) //���� �������� �� ������� ������ R (�������) 
        {
            Restart(); //����� ��� �������� ��� �������
        }
    }

    public void LoadOnBtnClick(string name) // ����� ������� ����������� � ������ ��� ������� ������� ����� ����� ������������� ����� ������� ������ �������� ����� � �������� name
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single); //�������� �����
    }
    
    public void Restart() //����� ��������
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, //����� ������ ������� �����
            LoadSceneMode.Single);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single); // �������� ����� ������ �����
    }
}
