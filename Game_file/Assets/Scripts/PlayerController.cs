using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    [Header("Скорость перемещения персонажа")]
    public float speed = 4f;

    [Header("Скорость бега персонажа")]
    public float runningSpeed = 7f;

    [Header("Сила прыжка")]
    public float jumpPower = 200f;

    [Header("Прыжок")]
    public bool ground;

    [HideInInspector]
    public bool canMove = true;

    public Rigidbody rb;

    public bool Sprint;
    // Update is called once per frame
    void Start(){
        playerSpeed = speed;
    }
    void Update()
    {
        GetInput();
    }

    void GetInput(){
        if(Input.GetKey(KeyCode.W)){
            transform.localPosition += transform.forward * speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.S)){
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.A)){
            transform.localPosition += -transform.right * speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D)){
            transform.localPosition += transform.right * speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(ground == true){
                rb.AddForce(transform.up * jumpPower);
            }
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            transform.localPosition += transform.forward * runningSpeed * Time.deltaTime;
            Sprint = true;
        }
        else{
            Sprint = false;
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Ground"){
            ground = true;
        }
    }

    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "Ground"){
            ground = false;
        }
    }
}
