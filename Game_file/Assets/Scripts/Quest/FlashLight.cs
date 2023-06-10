using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light light_flashlight; // свет фонарика
    public bool include_flashlight; // включение\выключение фонарика

    
    void Start()
    {
        Checking_the_Light();
    }

    
    void Update()
    {
        FlashLightOnOff();
    }

    // Включение и выключение фонарика
    public void FlashLightOnOff(){
        // Включение фонарика
        if(Input.GetKeyDown(KeyCode.F) && include_flashlight == false){
            light_flashlight.enabled = true;
            include_flashlight = true;
        }
        // Выключение фонарика
        else if(Input.GetKeyDown(KeyCode.F) && include_flashlight == true){
            light_flashlight.enabled = false;
            include_flashlight = false;
        }
    }

    // Проверка фонарика на включение или выключение
    public void Checking_the_Light(){
        if(include_flashlight == true){
            light_flashlight.enabled = true;
        }
        else light_flashlight.enabled = false;
    }
}
