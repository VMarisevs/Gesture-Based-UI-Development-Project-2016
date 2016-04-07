using UnityEngine;
using System.Collections;

public class SpawnPipes : MonoBehaviour {

    private static float MIN_HEIGHT = -12f;
    private static float MAX_HEIGHT = 30f;
    private static int PIPES_PER_LEVEL = 3;

    private LevelTime[] levels = {  new LevelTime(4f,9f), new LevelTime(3f,8f),
                                        new LevelTime(2f, 7f), new LevelTime(2f, 6f),
                                            new LevelTime(1.5f, 5f) };


    public GameObject pipes;
    public BirdGravity birdgravity;

    public GameScore score;

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

            Vector3 position = new Vector3(0, Random.Range(MIN_HEIGHT, MAX_HEIGHT),100);

            GameObject instance = Instantiate(pipes, position, Quaternion.identity) as GameObject;

            // getting current score and / by max pipes per level 
            int currentLevel = score.getScore() / PIPES_PER_LEVEL;

            // if this level is not defined, use last one
            if (currentLevel >= levels.Length)
                currentLevel = levels.Length - 1;

            // generating from level
            time = Random.Range(levels[currentLevel].getMin(), levels[currentLevel].getMax());

        }
    }

    private struct LevelTime
    {
        float min;// { public get { return min; } set { min = value; } }
        float max;// { get { return max; } set { max = value; } }

        public float getMin()
        {
            return min;
        }

        public float getMax()
        {
            return max;
        }

        public LevelTime(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
