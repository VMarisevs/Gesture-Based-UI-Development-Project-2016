using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject star;

    public float timeLeft = 30.0f;
    public float score = 0;

    public Text txtTimer;
    public Text txtScore;

    public List<GameObject> life = new List<GameObject>();

    void Start()
    {
        for (int i=0; i<5; i++)
        {
            insertLife();
        }

        updateScore(0);

        InvokeRepeating("updateTimer", 1.0f, 1.0f);
    }


    private void updateTimer()
    {
        timeLeft --;
        txtTimer.text = "Time remaining: " + timeLeft;
    }

    public void updateScore(int addscore)
    {
        score += addscore;
        txtScore.text = "Score: " + score;
        
        if (addscore != 0)
        {
            timeLeft += 3;
        }
    }

    public void insertLife()
    {
        Vector2 position = new Vector2(50 * (life.Count) + 50, 400);
        GameObject instance = Instantiate(star, position, Quaternion.identity) as GameObject;
        instance.transform.SetParent(gameObject.transform);
        life.Add(instance);
    }

    public void loseLife()
    {        

        if (life.Count > 0)
        {
            GameObject lastStar = life[life.Count - 1];
            life.Remove(lastStar);
            Destroy(lastStar);
        }
        else
        {
            print("Game over");

            // Application.LoadLevel("GameOver"); depricated method
            //SceneManager.LoadScene(1);
        }
    }

}
