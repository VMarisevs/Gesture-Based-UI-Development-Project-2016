using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObject : MonoBehaviour {

    public List<GameObject> spawnSpots;
    public List<GameObject> enemyObjects;

    // so enemy won't be able to spawn 2 in row from same area
    private bool enemySpawnBlock = true;


    private void spawnEnemies()
    {
        if (enemySpawnBlock)
        {
            enemySpawnBlock = false;

            bool spawned = false;
            int counter = 0;

            while (!spawned)
            {
                int spawnspot = Random.Range(0, spawnSpots.Count - 1);

                EnemySpawnArea spot = spawnSpots[spawnspot].GetComponentInChildren<EnemySpawnArea>();

                if (!spot.busy)
                {
                    spawnEnemy(spawnSpots[spawnspot]);
                  
                    spawned = true;
                   // gameManager.currentlevel.spawnedEnemies++;

                }

                // exits from a loop in case all spawn places busy
                if (counter++ > 6)
                    break;

            }

            // pause before spawn another object
            float randomDelay = Random.Range(1.5f, 3f);
            StartCoroutine(spawnWait(randomDelay));
        }
    }

    private IEnumerator spawnWait(float sec)
    {
        yield return new WaitForSeconds(sec);
        enemySpawnBlock = true;
    }

    private void spawnEnemy(GameObject spawnspot)
    {
        Vector3 position = spawnspot.transform.position;

        // randomly choose enemy
        int enemy = Random.Range(0, enemyObjects.Count-1);

        // instaciate
        GameObject instance = Instantiate(enemyObjects[enemy], position, Quaternion.identity) as GameObject;
        instance.transform.SetParent(gameObject.transform);
        
        //MoveEnemy mv = instance.GetComponentInChildren<MoveEnemy>();
        //mv.spawnArea = spawnspot;
    }



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        spawnEnemies();
    }
}
