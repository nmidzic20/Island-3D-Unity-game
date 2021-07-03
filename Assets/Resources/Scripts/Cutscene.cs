using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject cutsceneCam;
    public GameObject cutsceneCam2;
    public GameObject cutsceneCam3;
    // Start is called before the first frame update
    void Start()
    {
        cutsceneCam.SetActive(true);
        playerCam.SetActive(false);
        StartCoroutine(Intro());
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StopAllCoroutines();
            playerCam.SetActive(true);
            cutsceneCam.SetActive(false);
            cutsceneCam2.SetActive(false);
            cutsceneCam3.SetActive(false);
            this.enabled = false;
            //need to disable after it finishes so that spacebar doesn't 
            //interfere with CharController script
        }
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(5);
        cutsceneCam2.SetActive(true);
        cutsceneCam.SetActive(false);
        yield return new WaitForSeconds(5);
        cutsceneCam3.SetActive(true);
        cutsceneCam2.SetActive(false);
        yield return new WaitForSeconds(5);
        playerCam.SetActive(true);
        cutsceneCam3.SetActive(false);
        this.enabled = false;
        //need to disable after it finishes so that spacebar doesn't 
        //interfere with CharController script

    }
}
