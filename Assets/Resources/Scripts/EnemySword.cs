using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySword : MonoBehaviour
{
    public int hitsPlayer = 0;
    public Animator animCharacter;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Character");
        animCharacter = player.GetComponent<Animator>();   
        StartCoroutine(Heal());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character") {

            ++hitsPlayer;

            if (hitsPlayer == 6)
            {
                animCharacter.Play("die");
                CharController.playerAlive = false;
                StartCoroutine(Death());
            }
            
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Heal() //heal player every 20 sec
    {
        yield return new WaitForSeconds(20);
        if (hitsPlayer > 0) --hitsPlayer;

    }
}
