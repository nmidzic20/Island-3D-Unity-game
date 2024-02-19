using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject mook;
    private GameObject [] mooks = new GameObject [36];
    private EnemyAI [] mookScripts = new EnemyAI [36];
    public GameObject boss;
    private GameObject Boss;
    private EnemyAI bossScript;

    // Start is called before the first frame update
    void Start()
    {
        int terrainX = 0;
        int terrainZ = 0;
        int k = 0;

        //both loops give 36 mooks in total
        while (terrainZ < 800)
        { 
            for (int i = 0; i <= 5; i++)
            {
                mooks[k] = Instantiate(mook, new Vector3(200+terrainX, 
                    130, 0+terrainZ), Quaternion.identity);

                mookScripts[k] = mooks[k].GetComponent<EnemyAI>();
                ++k;
                terrainX += 150;
            }
            terrainZ += 150;
            terrainX = 0;  
        }

        Boss = Instantiate(boss, new Vector3(233, 250, 845), Quaternion.identity);
        bossScript = Boss.GetComponent<EnemyAI>();

        StartCoroutine(DestroyAfterDeath());
        
    }

    IEnumerator DestroyAfterDeath()
    {
        while (true) 
        {
            yield return new WaitForSeconds(10);
            for (int i = 0; i <= 35; i++)
            {
                if(!mookScripts[i].enemyAlive) Destroy(mooks[i]);
            }
            if (!bossScript.enemyAlive) Destroy(Boss);
        }
    }
}
