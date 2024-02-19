using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to enforce that the boss is defeated
public class Wall : MonoBehaviour
{
    public GameObject boss;
    EnemyAI script;
    void Start()
    {
        script = boss.GetComponent<EnemyAI>();
    }

    void OnCollisionEnter()
    {
        if (!script.enemyAlive)
        {
            //once the boss has been defeated, remove the barrier
            Destroy(gameObject); 
        }
    }
}
