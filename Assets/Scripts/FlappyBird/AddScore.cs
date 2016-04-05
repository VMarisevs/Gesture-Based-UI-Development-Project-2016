using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {

    public GameScore score;
    public Sounds sounds;

    void OnTriggerEnter(Collider other)
    {
        score.addScore();
        sounds.playCollected();
    }
}
