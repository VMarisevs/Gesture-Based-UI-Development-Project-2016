using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

    private int score = 0;

    public Text gameScore;

    public bool gameover = false;

    public GameObject txtGameover;

    public void addScore()
    {
        score += 1;
        gameScore.text = score.ToString();
    }

    public void setGameOver()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("pipe");

        foreach (GameObject pipe in pipes)
        {
            pipe.GetComponent<PipesMove>().gameover = true;
        }
        txtGameover.SetActive(true);
    }
}
