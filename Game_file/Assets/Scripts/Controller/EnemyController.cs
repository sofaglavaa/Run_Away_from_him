using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    float timer = 0;
    float stopwatch = 1;
    float flashlightOn = 0;

    // Настройка дистанции видимости игрока
    private float distance;
    [Space (-5)]
    [Header ("Target")]
    public Transform myPlayer;

    // Настройка радиуса видимости, атаки и зоны досягаемости
    [Space (-5)]
    [Header ("Distance")]
    public float radiusOfView = 10;
    public float attackDistance = 4f;
    public float reachArea = 15;

    // Подключение анимаций
    [Space (-5)]
    [Header ("Animations")]
    public string nameIdle;
    public string nameWalk;
    public string nameSword;
    public string nameAttack;

    // Панель при проигрыше
    // public float ActivePanel;
    public GameObject Panel_GaveOver;
    // Работа с рандомными точками для патрулирования
    public float speed;
    public List<Transform> moveSpots;
    private int randomSpot;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(myPlayer.position, transform.position); // Расстояние от игрока до противника
        // Первые 5 секунд противник стоит и далее начинает движение
        if(timer < 5){
            GetComponent <Animator>().Play(nameIdle);
            timer += Time.deltaTime;
        }
        else{
            // Патрулирование
            if(distance > radiusOfView){
                GetComponent <NavMeshAgent>().enabled = true;
                GetComponent <Animator>().Play(nameWalk);
                if(moveSpots.Count > 1){
                    if(moveSpots.Count > randomSpot){
                        GetComponent <NavMeshAgent>().SetDestination(moveSpots[randomSpot].position);
                        float distanceOfPoint = Vector3.Distance(transform.position, moveSpots[randomSpot].position);
                        if(distanceOfPoint > 2.5f){
                            GetComponent <Animator>().Play(nameWalk);
                            speed += Time.deltaTime * 3;
                        }
                        else if(distanceOfPoint <= 2.5f && distanceOfPoint >= 1f){
                            Vector3 direction = (moveSpots[randomSpot].position - transform.position).normalized;
                            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
                        }
                        else{
                            randomSpot++;
                        }
                        speed = Mathf.Clamp(speed, 0, 1);
                    }
                }
                // Поиск игрока при нажатии клавиши спринта
                if(Input.GetKeyDown(KeyCode.LeftShift)){
                    print("Проверка позиции игрока(спринт)" );
                    StartCoroutine(Search_Player());
                    GetComponent <NavMeshAgent>().destination = myPlayer.position;
                    GetComponent <Animator>().Play(nameWalk);
                }
                // Поиск игрока при нажатии клавиши ходьбы
                if(Input.GetKey(KeyCode.W)){
                    print(stopwatch);
                    stopwatch += Time.deltaTime;
                    if(stopwatch > 10){
                        print("Проверка позиции игрока(ходьба)" );
                        GetComponent <NavMeshAgent>().destination = myPlayer.position;
                        GetComponent <Animator>().Play(nameWalk);
                        stopwatch = 0;
                    }
                }
                // Поиск игрока при включении фонаря
                if(Input.GetKeyDown(KeyCode.F)){
                    flashlightOn += 1;
                    print(flashlightOn);
                    if(flashlightOn == 3){
                        print("Проверка позиции игрока(фонарик)" );
                        GetComponent <NavMeshAgent>().destination = myPlayer.position;
                        GetComponent <Animator>().Play(nameWalk);
                        flashlightOn = 0;
                    }
                }
            }
            // Преследование
            if(distance < radiusOfView & distance >= attackDistance){
                GetComponent <NavMeshAgent>().enabled = true;
                GetComponent <NavMeshAgent>().destination = myPlayer.position;
                GetComponent <Animator>().Play(nameSword);
                speed += 10;
                if (distance <= reachArea){
                    radiusOfView = radiusOfView + Time.deltaTime;
                }
            }
            // Атака игрока
            if(distance < attackDistance){
                // GetComponent <NavMeshAgent>().enabled = false;
                GetComponent <Animator>().Play(nameAttack);
                StartCoroutine(Active_Panel());
            }
            // Продолжение преследования
            if(distance > attackDistance & distance < radiusOfView){
                GetComponent <NavMeshAgent>().enabled = true;
                GetComponent <NavMeshAgent>().destination = myPlayer.position;
            }
        }
    }
    // Настройка уничтожения персонажа после срабатывания атаки
    IEnumerator Active_Panel(){
        yield return new WaitForSeconds(1.5f);
        Panel_GaveOver.SetActive(true);
    }
    // Настройка реакции для поиска игрока при спринте
    IEnumerator Search_Player(){
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            yield return new WaitForSeconds(1f);
        }
    }
}
