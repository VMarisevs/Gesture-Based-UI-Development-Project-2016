using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

    public AudioSource collected;
    public AudioSource flap;
    public AudioSource die;

    public void playCollected()
    {
        collected.Play();
    }

    public void playFlap()
    {
        flap.Play();
    }

    public void playDie()
    {
        die.Play();
    }
}
