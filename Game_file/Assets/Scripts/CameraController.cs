using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseX;
    private float mouseY;

    float xRotation = 0f;

    [Header("Настройки чувствительности")]
    public float sensitivityMouse = 2f;

    [Header("Выбор предмета(Игрока)")]
    public Transform Player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivityMouse;
        mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40f, 40f); 
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(-mouseY * new Vector3(1, 0, 0) * Time.deltaTime);

        Player.Rotate(mouseX * new Vector3(0, 1, 0));
    }
}
