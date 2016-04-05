using UnityEngine;
using System.Collections;

public class BirdHitPipe : MonoBehaviour {

    public BirdGravity birdGravity;

    void OnCollisionEnter(Collision collision)
    {
        birdGravity.dead = true;
    }


}
