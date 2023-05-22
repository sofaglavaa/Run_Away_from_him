using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(PauseGame){
                Resume();
            }
            else{
                Pause();
            }
        }
    }
    // Функция возврата к игре, если нажали кнопку "Continue"
    public void Resume(){
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }
    // Функция включения паузы и остановки игры с выводом меню
    public void Pause(){
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    // Функция возврата в главное меню при нажатии кнопки "Back menu"
    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
