using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    public GameObject [] prefabs; //didn't need to allocate here because editor has the list of them and knows its size (5)
    private GameObject [] newBall = new GameObject [8];
    public float speed = 5f, height = 0.5f;
    private float newY;
    private bool firstPass = true;

    // Start is called before the first frame update
    void Start()
    {        
        StartCoroutine(SpawnBalls());
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstPass) 
        {
            for(int i = 0; i < 8; i++)
            {
                newY = Mathf.Sin(Time.time * speed) * height + newBall[i].transform.position.y;
                newBall[i].transform.position = new Vector3(newBall[i].transform.position.x, newY, newBall[i].transform.position.z);
            }
        }
        
    }

    IEnumerator SpawnBalls()
    {
        while (true) 
        {
            if (!firstPass) for(int i = 0; i < 8; i++) Destroy(newBall[i]);

            for(int i = 0; i < 8; i++)
            {
                newBall[i] = Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(Random.Range(520, 620), 
                    Random.Range(127, 130), Random.Range(690, 760)), Quaternion.identity);
                firstPass = false;
            }


            yield return new WaitForSeconds(Random.Range(30, 90));
    

        }
    }

}
