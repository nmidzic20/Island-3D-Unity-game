using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public GameObject cam, cam2;
    private bool cam2Active;

    // Update is called once per frame
    void Update()
    {
        if (cam2Active) 
        {   
            cam2.transform.position -= cam2.transform.forward * Time.deltaTime * 1f;
            //go to victory screen
            if (cam2.transform.position.y > 250f) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

    }

    void OnTriggerEnter(Collider other) 
    {
        cam2.SetActive(true);
        cam.SetActive(false);
        cam2Active = true;
    }
}
