using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public GameObject camera; // Главная камера
    public float distance = 15f; // Расстояние взаимодействия с предметами
    GameObject currentWeapon; // Текущий предмет
    bool canPickUp = false; // Булева переменная отвечающая за возможность взять предмет

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUp(); // Если нажать клавишу E, то берем объект
        if (Input.GetKeyDown(KeyCode.T)) Drop(); // Если нажать клавишу T, то выбрасываем объект
    }

    // Функция взаимодействия с объектами
    void PickUp()
    {
        // Проверка попадания объекта в луч камеры
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance))
        {
            // Проверка объекта по тегу
            if(hit.transform.tag == "Weapon")
            {
                // Если есть предмет в руках, то его выбрасываем и берем новый в соответствии с настройками
                if (canPickUp) Drop();

                currentWeapon = hit.transform.gameObject;
                currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
                currentWeapon.GetComponent<Collider>().isTrigger = true;
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localEulerAngles = new Vector3(80f, 0f, 0f);
                canPickUp = true;
            }
        }

        
    }

    // Функция замены предмета
    void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon.GetComponent<Collider>().isTrigger = false;
        canPickUp = false;
        currentWeapon = null;
    } 
}
