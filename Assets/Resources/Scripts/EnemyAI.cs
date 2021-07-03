using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    float distance;
    bool playerIsInRange;
    public bool enemyAlive;
    float detectAmount = 40f;
    Animator anim;
    bool boss;

    public int hitsReceived = 0;
    public int hitsCritical;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Character").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.enabled = true;
        anim = GetComponent<Animator>();
        enemyAlive = true;

        if (gameObject.tag == "Boss")
        {
            boss = true;
            hitsCritical = 10;
        }
        else
        {
            boss = false;
            hitsCritical = 3;
        }
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        playerIsInRange = distance <= detectAmount;

        if (hitsReceived > hitsCritical) enemyAlive = false;


        if (CharController.playerAlive && enemyAlive && playerIsInRange)
        {
            nav.enabled = true;
            nav.SetDestination(player.position);
            transform.LookAt(player);

            if (distance < 2f) 
            {
                nav.enabled = false;
                if (boss) anim.Play("mixamo_com");
                else anim.Play("Attack1");
            }
            else 
            {
                //since this is all in playerIsInRange block, enemy will be moving
                //in any case because of nav, so play run animation
                if (boss) anim.Play("walk");
                else anim.Play("Run");
            }
            
        }
        else if (enemyAlive)
        {
            nav.enabled = false;

            if (boss) anim.Play("idle");
            else anim.Play("idle1");
        }
        else //enemy died
        {
            nav.enabled = false;

            if (boss) anim.Play("collapse");
            else anim.Play("Death2");
        }

    } 

}
