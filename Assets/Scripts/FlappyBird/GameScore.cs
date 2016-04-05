using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

    private int score = 0;

    public Text gameScore;

    public void addScore()
    {
        score += 1;
        gameScore.text = score.ToString();
    }
}
