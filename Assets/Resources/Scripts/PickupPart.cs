using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPart : MonoBehaviour
{

    public static int pieces = 0;
    public GameObject part;
    public GameObject piece, sword;
    public static bool hasSword;

    void Awake() 
    {
        //disable the sword the player is carrying until all the parts are picked up
        sword.SetActive(false);
        hasSword = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        piece = Instantiate(part, new Vector3(transform.position.x, 
            transform.position.y + 1f, transform.position.z - 0.5f), Quaternion.Euler(90, 0, 0));        
    }

    void OnCollisionEnter()
    {
        if (piece) pieces++;
        Destroy(piece);
        if (pieces == 3) 
        {
            sword.SetActive(true);
            hasSword = true;
        }

    }
}
