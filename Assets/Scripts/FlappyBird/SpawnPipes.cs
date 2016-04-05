using UnityEngine;
using System.Collections;

public class SpawnPipes : MonoBehaviour {

    public GameObject pipes;
    public BirdGravity birdgravity;

    private float time = 0;

	// Use this for initialization
	void Start () {
        InvokeRepeating("spawnpipes", 0.1f, 0.1f);
    }
	
	private void spawnpipes()
    {
        time -= 0.1f;

        if (time < 0 && !birdgravity.isDead())
        {

            Vector3 position = new Vector3(0, Random.Range(0f, 20f),100);

            GameObject instance = Instantiate(pipes, position, Quaternion.identity) as GameObject;

            time = Random.Range(4f, 9f);
        }
    }
}
