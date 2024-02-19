using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject OtherCircle, sword;
    private float x, y, z;
    public bool teleportActive;
    
    void Start()
    {
        teleportActive = true;
        x = OtherCircle.transform.position.x;
        y = OtherCircle.transform.position.y;
        z = OtherCircle.transform.position.z;
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Character") //allow only the player to teleport, not to boss too
        {
            if (teleportActive)
            {
                teleportActive = false;
                other.transform.position = new Vector3(x, y, z);
            }
            else teleportActive = true;
        }
    }

}
