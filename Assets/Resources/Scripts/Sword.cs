using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            //access the variable of that particular instantiated enemy
            EnemyAI script = other.GetComponent<EnemyAI>();
            script.hitsReceived++;
        }
    }
}
