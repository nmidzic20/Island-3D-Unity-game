using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        PickupPart.pieces = 0; //reset condition for collecting sword
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GetComponent<AudioSource>().mute = true; //so it does not overlap with play music
    }
}
