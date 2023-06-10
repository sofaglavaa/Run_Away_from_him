using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSteps : MonoBehaviour
{
    public AudioSource footSteps, stopFoot, sprintSteps;

    void Update(){
        if((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || ((Input.GetKey(KeyCode.D)))){
            if(Input.GetKey(KeyCode.LeftShift)) {
                footSteps.enabled = false;
                sprintSteps.enabled = true;
            }
            else if(Input.GetKey(KeyCode.Space)){
                footSteps.enabled = false;
                sprintSteps.enabled = false;
                stopFoot.enabled = true;
            }
            else {
                footSteps.enabled = true;
                sprintSteps.enabled = false;
            }
        }
        else {
                footSteps.enabled = false;
                sprintSteps.enabled = false;
                stopFoot.enabled = false;
            }
    }
}
