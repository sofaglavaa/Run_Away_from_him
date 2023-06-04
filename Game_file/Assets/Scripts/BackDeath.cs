using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackDeath : MonoBehaviour
{
    public bool Death;
    public GameObject panelGameOver;

    void Update()
    {
        if(Death){
            Pause();
        }
    }

    // Функция включения паузы и остановки игры с выводом меню
    public void Pause(){
        panelGameOver.SetActive(true);
        Time.timeScale = 0f;
        Death = true;
    }

    // Функция возврата в главное меню при нажатии кнопки "Back menu"
    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
