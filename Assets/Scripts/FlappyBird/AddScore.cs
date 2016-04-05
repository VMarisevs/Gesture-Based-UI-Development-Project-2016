using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {

    public GameScore score;

    void OnTriggerEnter(Collider other)
    {
        score.addScore();
    }
}
