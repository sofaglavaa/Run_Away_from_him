using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public GameObject notice;
    public GameObject noteUI;
    private void OnTriggerStay(Collider other)
    {
        // text.text = noteTextstr;
        if (Input.GetKey(KeyCode.E))
        {
            noteUI.SetActive(true);
        }
        if (Input.GetKey(KeyCode.T))
        {
            noteUI.SetActive(false);
        }
        notice.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        notice.SetActive(false);
        noteUI.SetActive(false);
    }
}
