using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Контроллер камеры
    [SerializeField] public float speedMouse = 300f;
    float xRot = 0f;
    float yRot = 0f;
    float mouseX;
    float mouseY;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Update()
    {
        //Управление курсором
        mouseX = Input.GetAxis("Mouse X") * speedMouse * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * speedMouse * Time.deltaTime;
        xRot -= mouseY;
        yRot += mouseX;
        xRot = Mathf.Clamp(xRot, -40f, 40f);
        yRot = Mathf.Clamp(yRot, -40f, 40f);
        transform.localRotation = Quaternion.Euler(xRot, yRot, 0f);
        //Приближение камеры по колесику мышки
        Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 100;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 20, 60);
    }
}
