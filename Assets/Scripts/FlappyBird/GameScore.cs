using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        try { 
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("pipe");

        foreach (GameObject pipe in pipes)
        {
            pipe.GetComponent<PipesMove>().gameover = true;
        }

        } catch(System.Exception e)
        {

        }
        txtGameover.SetActive(true);

        InvokeRepeating("backtomainmenu", 0.1f, 0.1f);
    }

    private float timer = 4f;

    private void backtomainmenu()
    {
        timer -= 0.1f;

        if (timer <= 0f)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }

}
