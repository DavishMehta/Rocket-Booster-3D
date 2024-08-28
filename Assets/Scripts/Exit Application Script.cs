using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApplicationScript : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("You have exited");
            Application.Quit();
        }
 
    }
}
